using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Enum;
using AhCha.Fortunate.Api.AppCode;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysNoticeDto;

namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统通知模块
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysNoticeController : BaseApiController
    {
        private readonly ISysNoticeService _iSysNoticeService;
        public SysNoticeController(ISysNoticeService iSysNoticeService)
        {
            _iSysNoticeService = iSysNoticeService;
        }


        /// <summary>
        /// 获取通知集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SqlSugarPagedList<SysNoticeOutput>> GetSysNoticePage([FromQuery] QuerySysNoticeInput input)
        {
            return await _iSysNoticeService.GetSysNoticePage(input);
        }

        /// <summary>
        /// 新增通知详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysNotice(AddSysNoticeInput input)
        {
            return await _iSysNoticeService.AddSysNotice(input);
        }

        /// <summary>
        /// 修改通知详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysNotice(PutSysNoticeInput input)
        {
            return await _iSysNoticeService.PutSysNotice(input);
        }

        /// <summary>
        /// 删除通知详情信息
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteSysUser(List<DeleteSysNoticeInput> input)
        {
            return await _iSysNoticeService.DeleteSysNotice(input);
        }

        /// <summary>
        /// 获取通知详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysNoticeOutput> GetSysNoticeDetail([FromQuery] SysNoticeInput input)
        {
            return await _iSysNoticeService.GetSysNoticeDetail(input);
        }

    }
}
