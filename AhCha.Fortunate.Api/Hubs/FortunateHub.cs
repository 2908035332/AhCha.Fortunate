using Microsoft.AspNetCore.SignalR;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.Common.Extensions;
using AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto;

namespace AhCha.Fortunate.Api.Hubs
{
    /// <summary>
    /// SignalR 即时通讯
    /// </summary>
    public class FortunateHub : Hub<IFortunateHubClient>
    {
        /// <summary>
        /// 连接 SignalR
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            HttpContext? _HttpContext = Context.GetHttpContext();
            string? token = _HttpContext?.Request.Query["access_token"];
            var data = TokenUtility.GetPrincipal(token);
            IOnlineUsersService iOnlineUsersService = AhChaFortunateGlobalContext.GetService<IOnlineUsersService>();
            PostOnlineUsersInput entity = new PostOnlineUsersInput()
            {
                UserId = Convert.ToInt64(data.FindFirst(ClaimConst.CLAINM_USERID)?.Value),
                Name = data.FindFirst(ClaimConst.CLAINM_NAME)?.Value,
                Account = data.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value,
                DeviceId = data.FindFirst(ClaimConst.CLAINM_DEVICE_ID)?.Value,
                ClientIP = _HttpContext?.GetIp(),
                SignalRId = Context.ConnectionId,
                CreateTime = DateTime.Now
            };
            List<string?> SignalRs = await iOnlineUsersService.ExistDeviceID(entity);
            //执行系统登出（由前端实现：ClientLoginOut）
            SignalRs.ForEach(async (x) => await Clients.Client(x).ClientLoginOut());
            await iOnlineUsersService.PostOnlineUsers(entity);
            _ = PeriodicTimerStartAsync();
        }

        /// <summary>
        /// 断开 SignalR
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            IOnlineUsersService iOnlineUsersService = AhChaFortunateGlobalContext.GetService<IOnlineUsersService>();
            await iOnlineUsersService.RemoveOnlineUsers(Context.ConnectionId);
        }

        /// <summary>
        /// 开启定时器（循环调用客户端函数）（可以根据用户id或查询在线用户表拿到链接id）
        /// </summary>
        /// <returns></returns>
        private async Task PeriodicTimerStartAsync()
        {
            using (var timer = new PeriodicTimer(TimeSpan.FromSeconds(1)))
            {
                string msg = $"---PeriodicTimer定时任务开启时间：{DateTime.Now}---";
                while (await timer.WaitForNextTickAsync())
                {
                    await Clients.All.SendMessage(msg);
                }
            }
        }
    }
}
