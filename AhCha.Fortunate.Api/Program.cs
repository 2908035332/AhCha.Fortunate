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

//雪花id配置
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = ushort.Parse("1") });

/*热加载*/
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

#region 初始化系统全局配置
InitConfiguration();
#endregion



//可解决：One or more validation errors occurred.
//关闭参数自动校验,我们需要返回自定义的格式
builder.Services.Configure<ApiBehaviorOptions>((option) =>
{
    option.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddControllers(option =>
{
    //异常简单处理
    option.Filters.Add(new ExceptionLogFilter());
    //记录接口请求记录
    option.Filters.Add(new ApiRequestLogFilter());
    //统一封装输出
    option.Filters.Add(new ResultFilter());
}).AddNewtonsoftJson(option =>
{
    // 设置大小写不变
    option.SerializerSettings.ContractResolver = null;
    // 格式化时间
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 忽略空值
    option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //雪花id丢失精度解决
    option.SerializerSettings.Converters.Add(new LongJsonConverter());
});
//定时任务
builder.Services.AddHostedService<PeriodicTaskService>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
CurrentUser.factory = builder.Services.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));

builder.Services.AddEndpointsApiExplorer();
//token
builder.Services.AddSingleton(new TokenUtility());
//SqlSugar
builder.Services.SqlSugarScopeConfigure();
#region 注册jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    //验证
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //是否验证Issuer
        ValidIssuer = AhChaFortunateGlobalContext.JwtSettings.Issuer,//发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = AhChaFortunateGlobalContext.JwtSettings.Audience,//订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AhChaFortunateGlobalContext.JwtSettings.SecretKey)), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
    };
    //配置自定义返回鉴权失败
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
            context.HandleResponse();
            //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
            object? ResponseMgs = Response.Mistake("无权限访问，请登录");
            //自定义返回的数据类型
            context.Response.ContentType = "application/json";
            //自定义返回状态码
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //输出Json数据结果
            context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseMgs));
            return Task.FromResult(0);
        }
    };
});
#endregion
//即时通讯
builder.Services.AddSignalR();
#region 配置Swagger

builder.Services.AddSwaggerGen(option =>
{
    //配置 Swagger
    AhChaFortunateGlobalContext.SwaggerConfigs.ForEach(entity =>
    {
        option.SwaggerDoc(entity.GroupName, new OpenApiInfo
        {
            Title = entity.Title,
            Version = entity.Version,
            Contact = new OpenApiContact()
            {
                Name = "后端Gitee地址链接",
                Url = new Uri("https://gitee.com/RunXiang/AhCha.Fortunate.git"),
            },
            License = new OpenApiLicense()
            {
                Name = "前端Gitee地址链接",
                Url = new Uri("https://gitee.com/RunXiang/AhCha.Fortunate.Web.git"),
            },
        });
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var flie = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(flie, true);
    option.OrderActionsBy(x => x.RelativePath);
    //Swagger 添加 jwt验证 --- "Bearer"
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,//jwt 默认存放authorization信息的位置（请求头）
        Type = SecuritySchemeType.ApiKey,
        Description = "JWT授权（数据将在请求头中进行传输）直接在下框中输入 Bearer{ token }（注意两者之间是一个空格）",
        Name = "Authorization",//jwt默认的参数名称

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

#region 替换容器  Autofac
#region 微软默认注入
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

#region 注册系统全局跨域
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
        //Swagger设置默认收缩起来
        option.DocExpansion(DocExpansion.None);
        //分组展示
        AhChaFortunateGlobalContext.SwaggerConfigs.ForEach(entity =>
        {
            option.SwaggerEndpoint($"/swagger/{entity.GroupName}/swagger.json", entity.Title);  //分组显示
        });
    });
}

//可全局获取单例服务
AhChaFortunateGlobalContext.Instance = app.Services;

app.UseHttpsRedirection();

app.UseRouting();

//跨域设置
app.UseCors("Cors");
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//启用倒带
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});
app.UseAuthentication();//认证
app.UseAuthorization();//授权

app.UseWebSockets();

app.MapHub<FortunateHub>("/Hubs/FortunateHub");

app.MapControllers();

app.Run();

#region 初始化全局静态配置
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
