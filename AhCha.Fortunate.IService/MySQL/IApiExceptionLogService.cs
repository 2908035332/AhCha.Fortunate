using AhCha.Fortunate.Entity.MySQL;
using AhCha.Fortunate.ModelsDto.MySQL.ApiExceptionLogDto;

namespace AhCha.Fortunate.IService.MySQL
{
    public interface IApiExceptionLogService : IBaseServices<ApiExceptionLog>
    {

        /// <summary>
        /// 新增ApiExceptionLog表数据
        /// </summary>
        Task PostApiExceptionLog(PostApiExceptionLogInput input);


    }
}
