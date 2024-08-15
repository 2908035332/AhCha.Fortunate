using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysNoticeDto;



namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysNoticeService : BaseServices<SysNotice>, ISysNoticeService
    {
        private readonly SqlSugarRepository<SysNotice> _TEntityRep;
        public SysNoticeService(SqlSugarRepository<SysNotice> TEntityRep)
        {
            _TEntityRep = TEntityRep;
        }

        /// <summary>
        /// 分页获取通知信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<SysNoticeOutput>> GetSysNoticePage(QuerySysNoticeInput input)
        {
            var query = await _TEntityRep.AsQueryable()
                .Select<SysNoticeOutput>()
                .ToPagedListAsync(input.PageIndex, input.PageSize);
            return query;
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSysNotice(List<DeleteSysNoticeInput> input)
        {
            var ids = input.Select(x => x.Id).ToList();
            int Execute = await _TEntityRep.DeleteAsync(x => ids.Contains(x.Id));
            return Execute > 0;
        }

        /// <summary>
        /// 新增通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddSysNotice(AddSysNoticeInput input)
        {
            var entity = input.Adapt<SysNotice>();
            return await _TEntityRep.InsertAsync(entity) > 0;
        }

        /// <summary>
        /// 编辑通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysNotice(PutSysNoticeInput input)
        {
            var entity = input.Adapt<SysNotice>();
            return await _TEntityRep.UpdateIgnoreNullAsync(entity) > 0;
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        /// <returns></returns>
        public async Task<SysNoticeOutput> GetSysNoticeDetail(SysNoticeInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            return entity.Adapt<SysNoticeOutput>();
        }

    }
}
