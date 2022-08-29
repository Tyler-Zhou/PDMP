using Microsoft.OpenApi.Models;
using PDMP.Server.Api.SwaggerFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PDMP API",
        Description = "PDMP API",
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
