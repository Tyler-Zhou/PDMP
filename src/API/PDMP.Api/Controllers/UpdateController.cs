using GeneralUpdate.AspNetCore.Services;
using GeneralUpdate.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 更新控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class UpdateController : ControllerBase
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger<UpdateController> _Logger;

        /// <summary>
        /// 更新服务
        /// </summary>
        private readonly IUpdateService _UpdateService;
        /// <summary>
        /// 更新控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="updateService">更新服务</param>
        public UpdateController(ILogger<UpdateController> logger, IUpdateService updateService)
        {
            _Logger = logger;
            _UpdateService = updateService;
        }

        /// <summary>
        /// https://localhost:5001/PDMP/Update/Versions/getUpdateVersions/1/1.1.1
        /// </summary>
        /// <param name="clientType">1:ClientApp 2:UpdateApp</param>
        /// <param name="clientVersion"></param>
        /// <param name="clientAppKey"></param>
        /// <returns></returns>
        [HttpGet("{clientType}/{clientVersion}")]
        public async Task<IActionResult> Versions(int clientType, string clientVersion, string clientAppKey)
        {
            _Logger.LogInformation("Client request 'Versions'.");
            var appSecretKey = "41A54379-C7D6-4920-8768-21A3468572E5";
            if (!appSecretKey.Equals(clientAppKey)) throw new Exception($"key {clientAppKey} is not found in the database, check whether you need to upload the new version information!");
            var resultJson = await _UpdateService.UpdateVersionsTaskAsync(clientType, clientVersion, UpdateVersions);
            return Ok(resultJson);
        }


        /// <summary>
        /// https://localhost:5001/PDMP/update/Validate/1/1.1.1
        /// </summary>
        /// <param name="appType">应用程序类型1:客户端程序(ClientApp) 2:更新程序(UpdateApp)</param>
        /// <param name="appVersion">应用程序版本</param>
        /// <param name="clientAppKey"></param>
        /// <returns></returns>
        [HttpGet("{appType}/{appVersion}")]
        public async Task<IActionResult> Validate(int appType, string appVersion, string clientAppKey)
        {
            _Logger.LogInformation("Client request 'Validate'.");
            var appSecretKey = "41A54379-C7D6-4920-8768-21A3468572E5";
            if (!appSecretKey.Equals(clientAppKey)) throw new Exception($"key {clientAppKey} is not found in the database, check whether you need to upload the new version information!");
            var lastVersion = GetLastVersion();
            var resultJson = await _UpdateService.UpdateValidateTaskAsync(appType, appVersion, lastVersion, true, GetValidateInfos);
            return Ok(resultJson);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<UpdateVersionDTO>> UpdateVersions(int clientType, string clientVersion)
        {
            //TODO:Link database query information.Different version information can be returned according to the 'clientType' of request.
            var results = new List<UpdateVersionDTO>();
            results.Add(new UpdateVersionDTO("1bfd7236258b12c51fd09f13808235df", 1626711760, "1.0.0.1", "http://localhost:5032/Bamboo/Download/GetFile?fileName=1.0.0.1.rar", "updatepacket1"));
            //results.Add(new UpdateVersionDTO("d9a3785f08ed3dd92872bd807ebfb917", 1626711820, "9.1.4.0",
            //"http://192.168.50.170/Update2.zip",
            //"updatepacket2"));
            //results.Add(new UpdateVersionDTO("224da586553d60315c55e689a789b7bd", 1626711880, "9.1.5.0",
            //"http://192.168.50.170/Update3.zip",
            //"updatepacket3"));
            return await Task.FromResult(results);
        }


        private async Task<List<UpdateVersionDTO>> GetValidateInfos(int clientType, string clientVersion)
        {
            //TODO:Link database query information.Different version information can be returned according to the 'clientType' of request.
            var results = new List<UpdateVersionDTO>();
            results.Add(new UpdateVersionDTO("1bfd7236258b12c51fd09f13808235df", 1626711760, "1.0.0.1", null, null));
            return await Task.FromResult(results);
        }

        private string GetLastVersion()
        {
            return "1.0.0.1";
        }
    }
}
