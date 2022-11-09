using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using PDMP.Api.Extensions;
using PDMP.Application;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{

}).ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();


//添加数据库服务到容器
builder.Services.AddDbContext<DbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectionString);
}).AddUnitOfWork<FrameworkDBContext, LibraryDBContext>();
//添加存储库到容器
builder.Services.AddCustomRepository<PermissionEntity, PermissionRepository>();
builder.Services.AddCustomRepository<RoleEntity, RoleRepository>();
builder.Services.AddCustomRepository<UserEntity, UserRepository>();
builder.Services.AddCustomRepository<RolePermissionEntity, RolePermissionRepository>();
builder.Services.AddCustomRepository<UserRoleEntity, UserRoleRepository>();
builder.Services.AddCustomRepository<MenuItemEntity, MenuItemRepository>();
//添加服务到容器
//Framework
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IVersionService, VersionService>();
//Library
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IFileService, FileService>();

//添加AutoMapper
var automapperConfog = new MapperConfiguration(config =>
{
    config.AddProfile(new PDMPProfile());
});
builder.Services.AddSingleton(automapperConfog.CreateMapper());

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents()
    {
        //Token验证失败
        OnAuthenticationFailed = context =>
        {
            //Token过期异常
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //响应头添加过期标识
                context.Response.Headers.Add("act", "expired");
            }
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = JwtClaimTypes.Name,
        ValidateIssuer = true, // 是否验证Issuer{false:本地不验证;true:远程验证}
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = builder.Configuration["Jwt:Audience"], //订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = false,
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "个人数据管理平台应用程序编程接口",
        Description = "个人数据管理平台应用程序编程接口",
        Version = "v1"
    });

    //启用注释功能
    var dirs = Directory.GetFiles(AppContext.BaseDirectory, "PDMP.*.xml").Where(item => (new FileInfo(item).Attributes & FileAttributes.Hidden) == 0).ToArray();
    foreach (var item in dirs)
    {
        c.IncludeXmlComments(item);
    }
    //排序
    c.OrderActionsBy(o => o.RelativePath);
    //添加对控制器的标签
    c.DocumentFilter<ChineseDocTag>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
//使用验证(JWT)
app.UseAuthentication();
//使用授权
app.UseAuthorization();

app.MapControllers();

app.Run();
