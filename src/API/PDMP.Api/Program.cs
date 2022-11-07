using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using PDMP.Api.Mapper;
using PDMP.Api.SwaggerFilter;
using PDMP.Application;
using PDMP.Contract.Interfaces;
using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;
using PDMP.Infrastructure.DBContent;
using PDMP.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{

}).ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();

//添加配置
builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDBSetting"));
builder.Services.Configure<FileSetting>(builder.Configuration.GetSection("FileSetting"));
//添加服务到容器
//添加数据库服务到容器
builder.Services.AddScoped<IMongoDBContext, MongoDBContext>();
//添加存储库到容器
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //是否验证Issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = builder.Configuration["Jwt:Audience"], //订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
    };
});

//添加实体映射
//添加AutoMapper
var automapperConfog = new MapperConfiguration(config =>
{
    config.AddProfile(new PDMPProfile());
});

builder.Services.AddSingleton(automapperConfog.CreateMapper());
//添加服务到容器
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IFileService, FileService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
