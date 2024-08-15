using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.Common.Utility;

namespace AhCha.Fortunate.Api.AppCode
{
    /// <summary>
    /// 定时任务（可能受iis回收机制影响）
    /// </summary>
    public class PeriodicTaskService : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _executingTask;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_cts.Token);

            // 如果取消令牌已经触发，则返回已完成的任务  
            if (cancellationToken.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            return Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var timer = new PeriodicTimer(TimeSpan.FromSeconds(1)))
            {
                Console.WriteLine($"---PeriodicTimer定时任务开启时间：{DateTime.Now}---");
                while (await timer.WaitForNextTickAsync(cancellationToken))
                {
                    var Hour = DateTime.Now.Hour;
                    var Microsecond = DateTime.Now.Minute;
                    var Second = DateTime.Now.Second;
                    if (Hour == AhChaFortunateGlobalContext.PeriodicTimerConfigs.Hour && Microsecond == AhChaFortunateGlobalContext.PeriodicTimerConfigs.Microsecond && Second == AhChaFortunateGlobalContext.PeriodicTimerConfigs.Second)
                    {
                        //Console.WriteLine($"---PeriodicTimer执行时间{Hour}:{Microsecond}:{Second}---");
                        //每天定时删除临时目录即其文件
                        string TempPath = Path.Combine(FileUtil.GetSystemDirectory, AhChaFortunateGlobalContext.DirectoryConfig.TempPath);
                        FileUtil.DeleteDirectory(TempPath);
                        //创建临时目录
                        FileUtil.CreateDirectory(TempPath);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();

            return _executingTask ?? Task.CompletedTask;
        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}
