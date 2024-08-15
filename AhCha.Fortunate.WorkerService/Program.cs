using AhCha.Fortunate.WorkerService;
using AhCha.Fortunate.WorkerService.Code;
var builder = Host.CreateApplicationBuilder(args);
//�� ASP.NET Core Ӧ�ó����й�Ϊ Windows ������Ҫ��װ Microsoft.Extensions.Hosting.WindowsServices
builder.Services.AddWindowsService();
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Environment.CurrentDirectory)
              //���𵽱��ط��񣨷�����������Ҫ����Ϊ����·��
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
builder.Services.AddHostedService<Worker>();

Global.GetPaths = configuration.GetSection("SetPaths").Get<List<string>>();
Global.ExecuteTimes = configuration.GetSection("ExecuteTime").Get<ExecuteTime>();

var host = builder.Build();
host.Run();
