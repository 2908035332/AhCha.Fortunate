using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.WorkerService.Code;

using Newtonsoft.Json;

namespace AhCha.Fortunate.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await PeriodicTimerStartAsync(stoppingToken);
        }

        private async Task PeriodicTimerStartAsync(CancellationToken stoppingToken)
        {
            LogUtil.Info($"开启成功：{JsonConvert.SerializeObject(Global.GetPaths)}");
            var nextTargetTime = GetNextTargetTime();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var delay = nextTargetTime - DateTime.Now;
                    if (delay <= TimeSpan.Zero)
                    {
                        // 当前时间已经过了目标时间，计算下一个目标时间  
                        nextTargetTime = GetNextTargetTime(nextTargetTime);
                        if (Global.GetPaths.Count > 0)
                        {   // 执行
                            Global.GetPaths.ForEach(GenerateKey.SetAesKey);
                        }
                    }
                    else
                    {
                        // 等待到下一个目标时间  
                        try
                        {
                            await Task.Delay(delay, stoppingToken);
                        }
                        catch (TaskCanceledException)
                        {
                            // 如果任务被取消，则退出循环  
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error($"{DateTime.Now}：执行报错 \r\n \r\n {JsonConvert.SerializeObject(ex)}");
                    break;
                }
            }
        }

        private DateTime GetNextTargetTime(DateTime? lastTime = null)
        {
            var now = lastTime ?? DateTime.Now;
            var todayTarget = new DateTime(now.Year, now.Month, now.Day, Global.ExecuteTimes.targetHour, Global.ExecuteTimes.targetMinute, Global.ExecuteTimes.targetSecond);
            if (now >= todayTarget)
            {
                todayTarget = todayTarget.AddDays(1);
            }
            return todayTarget;
        }
    }
}
