namespace AhCha.Fortunate.WorkerService.Code
{
    public class Global
    {
        public static List<string> GetPaths { get; set; }
        public static ExecuteTime ExecuteTimes { get; set; }
    }

    public class ExecuteTime
    {
        public int targetHour { get; set; }
        public int targetMinute { get; set; }
        public int targetSecond { get; set; }
    }
}
