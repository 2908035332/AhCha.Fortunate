using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MySQL;
using AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto;

namespace AhCha.Fortunate.IService.MySQL
{
    public interface IOnlineUsersService : IBaseServices<OnlineUsers>
    {

        /// <summary>
        /// 分页获取OnlineUsers数据
        /// </summary>
        Task<SqlSugarPagedList<OnlineUsersOutput>> GetOnlineUsersPage(QueryOnlineUsersInput input);

        /// <summary>
        /// 新增OnlineUsers表数据
        /// </summary>
        Task PostOnlineUsers(PostOnlineUsersInput input);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="SignalRId"></param>
        /// <returns></returns>
        Task RemoveOnlineUsers(string SignalRId);

        /// <summary>
        /// 设备id是否存在
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns>返回连接id集合</returns>
        Task<List<string?>> ExistDeviceID(PostOnlineUsersInput entity);

        /// <summary>
        /// 获取当前登录用的链接id
        /// </summary>
        /// <returns>返回连接id集合</returns>
        Task<List<string?>> GetUserSignalRIds();

    }
}
