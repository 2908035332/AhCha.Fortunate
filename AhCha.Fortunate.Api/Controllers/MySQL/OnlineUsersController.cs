using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto;

namespace AhCha.Fortunate.Api.Controllers.MySQL
{
    /// <summary>
    /// 在线用户
    /// </summary>
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class OnlineUsersController : BaseApiController
    {
        private readonly IOnlineUsersService _iOnlineUsersService;
        public OnlineUsersController(IOnlineUsersService iOnlineUsersService)
        {
            _iOnlineUsersService = iOnlineUsersService;
        }

        /// <summary>
        /// 分页获取在线用户数据
        /// </summary>
        [HttpGet]
        public async Task<SqlSugarPagedList<OnlineUsersOutput>> GetOnlineUsersPage([FromQuery] QueryOnlineUsersInput input)
        {
            return await _iOnlineUsersService.GetOnlineUsersPage(input);
        }

    }
}