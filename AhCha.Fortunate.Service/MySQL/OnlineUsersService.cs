using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MySQL;
using Microsoft.EntityFrameworkCore;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto;


namespace AhCha.Fortunate.Service.MySQL
{
    public class OnlineUsersService : BaseServices<OnlineUsers>, IOnlineUsersService
    {
        public readonly SqlSugarRepository<OnlineUsers> _TEntityRep;
        public OnlineUsersService(SqlSugarRepository<OnlineUsers> TEntityRep)
        {
            _TEntityRep = TEntityRep;
        }

        /// <summary>
        /// 设备id是否存在
        /// </summary>
        /// <param name="Account">当前账号</param>
        /// <param name="DeviceID">设备id</param>
        /// <returns>返回连接id集合</returns>
        public async Task<List<string?>> ExistDeviceID(PostOnlineUsersInput input)
        {
            List<string?> strings = new List<string?>();
            var entitys = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(input.Account, x.Account)).Select(x => new OnlineUsersOutput()
            {
                SignalRId = x.SignalRId,
                DeviceId = x.DeviceId,
            }).ToListAsync();
            int totalCount = entitys.Count();
            int deviceCount = entitys.Count(x => x.DeviceId == input.DeviceId);
            if (entitys != null && totalCount != deviceCount)
            {
                strings = entitys.Select(x => x.SignalRId).ToList();
            }
            return strings;
        }

        /// <summary>
        /// 分页获取OnlineUsers数据
        /// </summary>
        public async Task<SqlSugarPagedList<OnlineUsersOutput>> GetOnlineUsersPage(QueryOnlineUsersInput input)
        {
            var query = await _TEntityRep.AsQueryable().OrderByDescending(x => x.CreateTime)
            .Select<OnlineUsersOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
            return query;
        }

        /// <summary>
        /// 获取当前登录用的链接id
        /// </summary>
        /// <returns>返回连接id集合</returns>
        public async Task<List<string?>> GetUserSignalRIds()
        {
            return await _TEntityRep.AsQueryable().Where(x => x.UserId == CurrentUser.GetUserId).Select(x => x.SignalRId).ToListAsync();
        }

        /// <summary>
        /// 新增OnlineUsers数据
        /// </summary>
        public async Task PostOnlineUsers(PostOnlineUsersInput input)
        {
            try
            {
                var entity = input.Adapt<OnlineUsers>();
                await _TEntityRep.InsertAsync(entity);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="SignalRId"></param>
        /// <returns></returns>
        public async Task RemoveOnlineUsers(string SignalRId)
        {
            try
            {
                await _TEntityRep.DeleteAsync(x => SqlFunc.Equals(x.SignalRId, SignalRId));
            }
            catch
            {
            }
        }
    }
}
