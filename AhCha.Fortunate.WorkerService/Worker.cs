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
            LogUtil.Info($"�����ɹ���{JsonConvert.SerializeObject(Global.GetPaths)}");
            var nextTargetTime = GetNextTargetTime();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var delay = nextTargetTime - DateTime.Now;
                    if (delay <= TimeSpan.Zero)
                    {
                        // ��ǰʱ���Ѿ�����Ŀ��ʱ�䣬������һ��Ŀ��ʱ��  
                        nextTargetTime = GetNextTargetTime(nextTargetTime);
                        if (Global.GetPaths.Count > 0)
                        {   // ִ��
                            Global.GetPaths.ForEach(GenerateKey.SetAesKey);
                        }
                    }
                    else
                    {
                        // �ȴ�����һ��Ŀ��ʱ��  
                        try
                        {
                            await Task.Delay(delay, stoppingToken);
                        }
                        catch (TaskCanceledException)
                        {
                            // �������ȡ�������˳�ѭ��  
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error($"{DateTime.Now}��ִ�б��� \r\n \r\n {JsonConvert.SerializeObject(ex)}");
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
