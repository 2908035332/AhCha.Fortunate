using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysNoticeDto;


namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ISysNoticeService : IBaseServices<SysNotice>
    {
        /// <summary>
        /// 分页获取通知信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SqlSugarPagedList<SysNoticeOutput>> GetSysNoticePage(QuerySysNoticeInput input);

        /// <summary>
        /// 新增通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddSysNotice(AddSysNoticeInput input);

        /// <summary>
        /// 编辑通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysNotice(PutSysNoticeInput input);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSysNotice(List<DeleteSysNoticeInput> input);

        /// <summary>
        /// 获取通知详情
        /// </summary>
        /// <returns></returns>
        Task<SysNoticeOutput> GetSysNoticeDetail(SysNoticeInput input);
    }
}
