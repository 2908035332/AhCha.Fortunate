using AhCha.Fortunate.WorkerService;
using AhCha.Fortunate.WorkerService.Code;
var builder = Host.CreateApplicationBuilder(args);
//将 ASP.NET Core 应用程序托管为 Windows 服务。需要安装 Microsoft.Extensions.Hosting.WindowsServices
builder.Services.AddWindowsService();
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Environment.CurrentDirectory)
              //部署到本地服务（服务器），需要更改为绝对路径
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
builder.Services.AddHostedService<Worker>();

Global.GetPaths = configuration.GetSection("SetPaths").Get<List<string>>();
Global.ExecuteTimes = configuration.GetSection("ExecuteTime").Get<ExecuteTime>();

var host = builder.Build();
host.Run();
