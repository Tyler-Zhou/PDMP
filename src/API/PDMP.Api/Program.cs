using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
