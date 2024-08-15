namespace AhCha.Fortunate.Api.Hubs
{
    /// <summary>
    /// 客户端定义的函数，js函数（单向）
    /// </summary>
    public interface IFortunateHubClient
    {
        /// <summary>
        /// 调用客户端退出方法
        /// </summary>
        /// <returns></returns>
        Task ClientLoginOut();

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        Task SendMessage(string Msg);
    }
}
