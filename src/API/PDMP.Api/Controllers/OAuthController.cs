/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:45:25
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PDMP.Contract;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    [Authorize]
    public class OAuthController : ControllerBase
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        readonly string _Audience = "";
        /// <summary>
        /// 
        /// </summary>
        readonly string _Issuer = "";
        /// <summary>
        /// 
        /// </summary>
        readonly string _SecretKey = "";
        /// <summary>
        /// 
        /// </summary>
        readonly int _TokenValidityInMinutes = 0;
        /// <summary>
        /// 
        /// </summary>
        readonly int _RefreshTokenValidityInDays = 0;
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService _UserService;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<OAuthController> _Logger;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 账号控制器
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="userService"></param>
        public OAuthController(ILogger<OAuthController> logger, IConfiguration configuration, IUserService userService)
        {
            _Logger = logger;
            _UserService = userService;

            _Audience = configuration["Jwt:Audience"];
            _Issuer = configuration["Jwt:Issuer"];
            _SecretKey = configuration["Jwt:SecretKey"];
            _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out _TokenValidityInMinutes);
            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out _RefreshTokenValidityInDays);
        }
        #endregion

        #region 公用方法(Public Method)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Login([FromBody] LoginInfo param)
        {
            try
            {
                UserInfo model = await _UserService.AuthorizationAsync(param.Name, param.Password);

                var authTime = DateTime.UtcNow;
                var expiresAt = authTime.AddMinutes(_TokenValidityInMinutes);
                var authClaims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Audience,_Audience),
                        new Claim(JwtClaimTypes.Issuer,_Issuer),
                        new Claim(JwtClaimTypes.Id, $"{model.Id}"),
                        new Claim(JwtClaimTypes.Name, model.Name),
                        new Claim(JwtClaimTypes.Email, model.Email),
                        new Claim(JwtClaimTypes.PhoneNumber, model.PhoneNumber)
                    };
                var token = GenerateAccessToken(authClaims, expiresAt);
                var refreshToken = GenerateRefreshToken();

                model.RefreshToken = refreshToken;
                model.RefreshTokenExpiryTime = authTime.AddDays(_RefreshTokenValidityInDays);

                await _UserService.UpdateAsync(model);
                return new ApiResponse(true, new TokenResponse 
                {
                    UserId = model.Id,
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    Expiration = expiresAt,
                }
                );
            }
            catch (Exception ex)
            {
                return new ApiResponse($"登录发生异常:{ex.Message}", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> RefreshToken([FromBody] TokenRequest param)
        {
            try
            {
                if (param is null)
                {
                    throw new Exception("Invalid client request");
                }

                string accessToken = param.AccessToken;
                string refreshToken = param.RefreshToken;

                var principal = GetPrincipalFromAccessToken(accessToken);
                if (principal == null)
                {
                    throw new Exception("Invalid access token or refresh token");
                }

                string username = principal.Identity.Name;

                var model = await _UserService.GetSingleAsync(username);
                var authTime = DateTime.UtcNow;

                if (model == null || model.RefreshToken != refreshToken || model.RefreshTokenExpiryTime <= authTime)
                {
                    throw new Exception("Invalid access token or refresh token");
                }
                var expiresAt = authTime.AddMinutes(_TokenValidityInMinutes);

                var newToken = GenerateAccessToken(principal.Claims.ToArray(), expiresAt);
                var newRefreshToken = GenerateRefreshToken();

                model.RefreshToken = newRefreshToken;
                model.RefreshTokenExpiryTime = authTime.AddDays(_RefreshTokenValidityInDays);

                await _UserService.UpdateAsync(model);
                return new ApiResponse(true, new TokenResponse
                {
                    AccessToken = newToken,
                    RefreshToken = newRefreshToken,
                    Expiration = expiresAt,
                }
                );
            }
            catch (Exception ex)
            {
                return new ApiResponse($"刷新Token发生异常:{ex.Message}", false);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> LoginOut(long userId)
        {
            try
            {
                var model = await _UserService.GetSingleAsync(userId);
                model.RefreshToken = "";
                model.RefreshTokenExpiryTime = DateTime.Now;
                await _UserService.UpdateAsync(model);
                return new ApiResponse(true, "");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"用户[{userId}]登出时发生异常:{ex.Message}", false);
            }
        }
        #endregion

        #region 私有方法(Private Method)
        /// <summary>
        /// 生成刷新Token
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        /// <param name="expiresAt"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GenerateAccessToken(Claim[] claims,DateTime expiresAt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 从Token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipalFromAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_SecretKey)),
                    ValidateLifetime = false
                };
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}