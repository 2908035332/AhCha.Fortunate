namespace AhCha.Fortunate.ModelsDto
{
    /// <summary>
    /// 响应输出
    /// </summary>
    public class Response
    {
        private Response()
        {

        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回提示信息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 0失败，1成功（用于前端判断）
        /// </summary>
        public Code Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="Message">提示信息</param>
        /// <returns></returns>
        public static Response Correct(string Message)
        {
            return new Response()
            {
                Success = true,
                Message = string.IsNullOrEmpty(Message) ? "操作成功" : Message,
                Code = Code.成功,
                Data = null,
                Timestamp = DateTime.Now.Millisecond,
            };
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="Data">数据</param>
        /// <param name="Message">提示信息</param>
        /// <returns></returns>
        public static Response Correct(object? Data = null, string Message = "")
        {
            return new Response()
            {
                Success = true,
                Message = string.IsNullOrEmpty(Message) ? "操作成功" : Message,
                Code = Code.成功,
                Data = Data,
                Timestamp = DateTime.Now.Millisecond,
            };
        }

        /// <summary>
        /// 返回错误提示
        /// </summary>
        /// <param name="Message">错误信息</param>
        /// <returns></returns>
        public static object Mistake(string Message = "")
        {
            return new Response()
            {
                Success = false,
                Code = Code.失败,
                Message = string.IsNullOrEmpty(Message) ? "请求失败" : Message,
                Data = null,
                Timestamp = DateTime.Now.Millisecond,
            };
        }
    }

    public enum Code
    {
        成功,
        失败
    }

}
