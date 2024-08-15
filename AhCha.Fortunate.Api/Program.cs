using Autofac;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using Yitter.IdGenerator;
using AhCha.Fortunate.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using AhCha.Fortunate.Api.Hubs;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Api.AppCode;
using AhCha.Fortunate.Common.Global;
using Microsoft.IdentityModel.Tokens;
using AhCha.Fortunate.Common.Utility;
using Swashbuckle.AspNetCore.SwaggerUI;
using AhCha.Fortunate.Repositories.SqlSugar;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;



var builder = WebApplication.CreateBuilder(args);

//ѩ��id����
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = ushort.Parse("1") });

/*�ȼ���*/
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

#region ��ʼ��ϵͳȫ������
InitConfiguration();
#endregion



//�ɽ����One or more validation errors occurred.
//�رղ����Զ�У��,������Ҫ�����Զ���ĸ�ʽ
builder.Services.Configure<ApiBehaviorOptions>((option) =>
{
    option.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddControllers(option =>
{
    //�쳣�򵥴���
    option.Filters.Add(new ExceptionLogFilter());
    //��¼�ӿ������¼
    option.Filters.Add(new ApiRequestLogFilter());
    //ͳһ��װ���
    option.Filters.Add(new ResultFilter());
}).AddNewtonsoftJson(option =>
{
    // ���ô�Сд����
    option.SerializerSettings.ContractResolver = null;
    // ��ʽ��ʱ��
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // ���Կ�ֵ
    option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //ѩ��id��ʧ���Ƚ��
    option.SerializerSettings.Converters.Add(new LongJsonConverter());
});
//��ʱ����
builder.Services.AddHostedService<PeriodicTaskService>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
CurrentUser.factory = builder.Services.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));

builder.Services.AddEndpointsApiExplorer();
//token
builder.Services.AddSingleton(new TokenUtility());
//SqlSugar
builder.Services.SqlSugarScopeConfigure();
#region ע��jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    //��֤
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //�Ƿ���֤Issuer
        ValidIssuer = AhChaFortunateGlobalContext.JwtSettings.Issuer,//������Issuer
        ValidateAudience = true, //�Ƿ���֤Audience
        ValidAudience = AhChaFortunateGlobalContext.JwtSettings.Audience,//������Audience
        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AhChaFortunateGlobalContext.JwtSettings.SecretKey)), //SecurityKey
        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
        RequireExpirationTime = true,
    };
    //�����Զ��巵�ؼ�Ȩʧ��
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            //�˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ�����������ҪŶ������
            context.HandleResponse();
            //�Զ����Լ���Ҫ���ص����ݽ����������Ҫ���ص���Json����ͨ������Newtonsoft.Json�����ת��
            object? ResponseMgs = Response.Mistake("��Ȩ�޷��ʣ����¼");
            //�Զ��巵�ص���������
            context.Response.ContentType = "application/json";
            //�Զ��巵��״̬��
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //���Json���ݽ��
            context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseMgs));
            return Task.FromResult(0);
        }
    };
});
#endregion
//��ʱͨѶ
builder.Services.AddSignalR();
#region ����Swagger

builder.Services.AddSwaggerGen(option =>
{
    //���� Swagger
    AhChaFortunateGlobalContext.SwaggerConfigs.ForEach(entity =>
    {
        option.SwaggerDoc(entity.GroupName, new OpenApiInfo
        {
            Title = entity.Title,
            Version = entity.Version,
            Contact = new OpenApiContact()
            {
                Name = "���Gitee��ַ����",
                Url = new Uri("https://gitee.com/RunXiang/AhCha.Fortunate.git"),
            },
            License = new OpenApiLicense()
            {
                Name = "ǰ��Gitee��ַ����",
                Url = new Uri("https://gitee.com/RunXiang/AhCha.Fortunate.Web.git"),
            },
        });
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var flie = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(flie, true);
    option.OrderActionsBy(x => x.RelativePath);
    //Swagger ��� jwt��֤ --- "Bearer"
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,//jwt Ĭ�ϴ��authorization��Ϣ��λ�ã�����ͷ��
        Type = SecuritySchemeType.ApiKey,
        Description = "JWT��Ȩ�����ݽ�������ͷ�н��д��䣩ֱ�����¿������� Bearer{ token }��ע������֮����һ���ո�",
        Name = "Authorization",//jwtĬ�ϵĲ�������

    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }

    });
});
#endregion

#region �滻����  Autofac
#region ΢��Ĭ��ע��
/*
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IFilesTableService, FilesTableService>();
builder.Services.AddTransient<ILoginService, LoginService>();
*/
#endregion
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((context, containerbuilder) =>
{
    containerbuilder.RegisterModule<DependencyAutoInjection>();
});
#endregion

#region ע��ϵͳȫ�ֿ���
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});
#endregion

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
if (AhChaFortunateGlobalContext.isTest)
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        //Swagger����Ĭ����������
        option.DocExpansion(DocExpansion.None);
        //����չʾ
        AhChaFortunateGlobalContext.SwaggerConfigs.ForEach(entity =>
        {
            option.SwaggerEndpoint($"/swagger/{entity.GroupName}/swagger.json", entity.Title);  //������ʾ
        });
    });
}

//��ȫ�ֻ�ȡ��������
AhChaFortunateGlobalContext.Instance = app.Services;

app.UseHttpsRedirection();

app.UseRouting();

//��������
app.UseCors("Cors");
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//���õ���
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});
app.UseAuthentication();//��֤
app.UseAuthorization();//��Ȩ

app.UseWebSockets();

app.MapHub<FortunateHub>("/Hubs/FortunateHub");

app.MapControllers();

app.Run();

#region ��ʼ��ȫ�־�̬����
void InitConfiguration()
{
    AhChaFortunateGlobalContext.Configuration = configuration;
    AhChaFortunateGlobalContext.AhChaFortunateContext = configuration.GetSection("AhChaFortunateContext").Get<String>();
    AhChaFortunateGlobalContext.DatabaseConfigs = configuration.GetSection("DatabaseConfigs").Get<List<DatabaseConfig>>();
    AhChaFortunateGlobalContext.RedisConnection = configuration.GetSection("RedisConnection").Get<String>();
    AhChaFortunateGlobalContext.isTest = configuration.GetSection("isTest").Get<String>() == "1";
    AhChaFortunateGlobalContext.RestUserPwd = configuration.GetSection("RestUserPwd").Get<String>();
    AhChaFortunateGlobalContext.SM2Config = configuration.GetSection("SM2Config").Get<SM2Config>();
    AhChaFortunateGlobalContext.SM4Config = configuration.GetSection("SM4Config").Get<SM4Config>();
    AhChaFortunateGlobalContext.DirectoryConfig = configuration.GetSection("DirectoryConfig").Get<DirectoryConfig>();
    AhChaFortunateGlobalContext.RsaConfig = configuration.GetSection("RsaConfig").Get<RsaConfig>();
    AhChaFortunateGlobalContext.AesKey = configuration.GetSection("AesKey").Get<String>();
    AhChaFortunateGlobalContext.AliyunOssConfig = configuration.GetSection("AliyunOssConfig").Get<AliyunOssConfig>();
    AhChaFortunateGlobalContext.SwaggerConfigs = configuration.GetSection("SwaggerConfig").Get<List<SwaggerConfig>>();
    AhChaFortunateGlobalContext.JwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
    AhChaFortunateGlobalContext.PeriodicTimerConfigs = configuration.GetSection("PeriodicTimerConfig").Get<PeriodicTimerConfig>();
}
#endregion
