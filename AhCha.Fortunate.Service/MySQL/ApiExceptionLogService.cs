using Mapster;
using AhCha.Fortunate.Entity.MySQL;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MySQL.ApiExceptionLogDto;

namespace AhCha.Fortunate.Service.MySQL
{
    public class ApiExceptionLogService : BaseServices<ApiExceptionLog>, IApiExceptionLogService
    {
        /// <summary>
        /// 新增ApiExceptionLog数据
        /// </summary>
        public async Task PostApiExceptionLog(PostApiExceptionLogInput input)
        {
            using var db = SqlSugarContext.GetInstance(GetMySQLConn);
            var entity = input.Adapt<ApiExceptionLog>();
            await db.Insertable<ApiExceptionLog>(entity).ExecuteCommandAsync();
        }
    }
}
