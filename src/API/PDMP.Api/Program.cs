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

//�������
builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDBSetting"));
builder.Services.Configure<FileSetting>(builder.Configuration.GetSection("FileSetting"));
//��ӷ�������
//������ݿ��������
builder.Services.AddScoped<IMongoDBContext, MongoDBContext>();
//��Ӵ洢�⵽����
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
//���ʵ��ӳ��
//���AutoMapper
var automapperConfog = new MapperConfiguration(config =>
{
    config.AddProfile(new PDMPProfile());
});

builder.Services.AddSingleton(automapperConfog.CreateMapper());
//��ӷ�������
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
        Title = "�������ݹ���ƽ̨Ӧ�ó����̽ӿ�",
        Description = "�������ݹ���ƽ̨Ӧ�ó����̽ӿ�",
        Version = "v1"
    });

    //����ע�͹���
    var dirs = Directory.GetFiles(AppContext.BaseDirectory, "PDMP.*.xml").Where(item => (new FileInfo(item).Attributes & FileAttributes.Hidden) == 0).ToArray();
    foreach (var item in dirs)
    {
        c.IncludeXmlComments(item);
    }
    //����
    c.OrderActionsBy(o => o.RelativePath);
    //��ӶԿ������ı�ǩ
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
