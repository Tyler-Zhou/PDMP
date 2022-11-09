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


//������ݿ��������
builder.Services.AddDbContext<DbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectionString);
}).AddUnitOfWork<FrameworkDBContext, LibraryDBContext>();
//��Ӵ洢�⵽����
builder.Services.AddCustomRepository<PermissionEntity, PermissionRepository>();
builder.Services.AddCustomRepository<RoleEntity, RoleRepository>();
builder.Services.AddCustomRepository<UserEntity, UserRepository>();
builder.Services.AddCustomRepository<RolePermissionEntity, RolePermissionRepository>();
builder.Services.AddCustomRepository<UserRoleEntity, UserRoleRepository>();
builder.Services.AddCustomRepository<MenuItemEntity, MenuItemRepository>();
//��ӷ�������
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

//���AutoMapper
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
        //Token��֤ʧ��
        OnAuthenticationFailed = context =>
        {
            //Token�����쳣
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //��Ӧͷ��ӹ��ڱ�ʶ
                context.Response.Headers.Add("act", "expired");
            }
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = JwtClaimTypes.Name,
        ValidateIssuer = true, // �Ƿ���֤Issuer{false:���ز���֤;true:Զ����֤}
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //������Issuer
        ValidateAudience = true, //�Ƿ���֤Audience
        ValidAudience = builder.Configuration["Jwt:Audience"], //������Audience
        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
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
//ʹ����֤(JWT)
app.UseAuthentication();
//ʹ����Ȩ
app.UseAuthorization();

app.MapControllers();

app.Run();
