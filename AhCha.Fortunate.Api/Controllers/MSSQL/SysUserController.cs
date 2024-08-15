using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.ModelsDto.MSSQL.SysUsersDto;

namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统用户模块
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysUserController : BaseApiController
    {

        private readonly ISysUsersService _iUsersService;
        public SysUserController(ISysUsersService iUsersService) => _iUsersService = iUsersService;

        /// <summary>
        /// 获取用户菜单 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserTreeMenuOutput>> GetUserRoute()
        {
            return await _iUsersService.GetUserRoute();
        }

        /// <summary>
        /// 获取用户按钮信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserButton>> GetUserBtns()
        {
            return await _iUsersService.GetUserBtns();
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<LoginUserInfo> GetLoginUserInfo()
        {
            return await _iUsersService.GetLoginUserInfo();
        }

        /// <summary>
        /// 获取用户数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SqlSugarPagedList<SysUserOutput>> GetSysUserPage([FromQuery] QuerySysUsersInput input)
        {
            return await _iUsersService.GetSysUserPage(input);
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysUser(AddSysUserInput input)
        {
            return await _iUsersService.AddSysUser(input);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysUser(PutSysUserInput input)
        {
            return await _iUsersService.PutSysUser(input);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteSysUser(List<DeleteSysUserInput> inputs)
        {
            return await _iUsersService.DeleteSysUser(inputs);
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysUserOutput> GetSysUserDetail([FromQuery] SysUserInput input)
        {
            return await _iUsersService.GetSysUserDetail(input);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<string> RestUserPwd(SysUserInput input)
        {
            return await _iUsersService.RestUserPwd(input);
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServerInfo GetServerInfo()
        {
            ServerInfo serverInfo = new ServerInfo()
            {
                MachineName = ServerInfoUtil.MachineName,
                OSName = ServerInfoUtil.OSName,
                OSArchitecture = ServerInfoUtil.OSArchitecture,
                DoNetName = ServerInfoUtil.DoNetName,
                IP = ServerInfoUtil.IP[0],
                CpuCount = ServerInfoUtil.CpuCount,
                UseRam = ServerInfoUtil.UseRam,
                StartTime = ServerInfoUtil.StartTime,
                RunTime = ServerInfoUtil.RunTime,
                DiskInfo = ServerInfoUtil.DiskInfo,
                MemoryInfo = ServerInfoUtil.MemoryInfo
            };
            return serverInfo;
        }

        /// <summary>
        /// 当前用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> PutSysUserPwd(PutSysUserPwdInput input)
        {
            return await _iUsersService.PutSysUserPwd(input);
        }

        /// <summary>
        /// 获取日历
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<HolidaysModel>> GetHolidays(string year)
        {
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new Exception("非法请求");
            }
            //需要返回的数据
            List<HolidaysModel> Holidays = new List<HolidaysModel>();
            //月份
            for (int mon = 1; mon <= 12; mon++)
            {
                //拼接 年-月  以 - 为区分
                string month = (mon <= 9 ? "0" + mon : mon.ToString());
                string YearMonth = year + "-" + month;
                //天
                for (int day = 1; day <= MonthDays(Convert.ToInt32(year), mon); day++)
                {
                    HolidaysModel holidaysModel = new HolidaysModel();

                    string dayStr = day <= 9 ? ("0" + day) : day.ToString();
                    if (day <= 9)
                    {
                        holidaysModel.holidayDate = YearMonth + "-0" + day;
                    }
                    else
                    {
                        holidaysModel.holidayDate = YearMonth + "-" + day;
                    }
                    holidaysModel.Week = Convert.ToDateTime(YearMonth + "-" + day).DayOfWeek.ToString();
                    Holidays.Add(holidaysModel);
                }
            }
            return Task.FromResult(Holidays);
        }
    }
}
