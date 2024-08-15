using Mapster;
using AhCha.Fortunate.Entity.MySQL;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MySQL.ApiRequestLogDto;



namespace AhCha.Fortunate.Service.MySQL
{
    public class ApiRequestLogService : BaseServices<ApiRequestLog>, IApiRequestLogService
    {
        /// <summary>
        /// 新增ApiRequestLog数据
        /// </summary>
        public async Task PostApiRequestLog(PostApiRequestLogInput input)
        {
            using var db = SqlSugarContext.GetInstance(GetMySQLConn);
            var entity = input.Adapt<ApiRequestLog>();
            await db.Insertable<ApiRequestLog>(entity).ExecuteCommandAsync();
        }
    }
}
