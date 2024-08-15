using AhCha.Fortunate.Entity.MySQL;
using AhCha.Fortunate.ModelsDto.MySQL.ApiRequestLogDto;

namespace AhCha.Fortunate.IService.MySQL
{
    public interface IApiRequestLogService : IBaseServices<ApiRequestLog>
    {

        /// <summary>
        /// 新增ApiRequestLog表数据
        /// </summary>
        Task PostApiRequestLog(PostApiRequestLogInput input);

    }
}
