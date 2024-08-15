
namespace AhCha.Fortunate.Entity
{
    /// <summary>
    /// 数据库标识配置，需要与appsettings.json数据库配置ConfigId字段数据一致
    /// </summary>
    public class ConstConfigId
    {
        /// <summary>
        /// Mysql数据库标识
        /// </summary>
        public const string MySqlAhChaFortunate = "MySqlAhChaFortunate";

        /// <summary>
        /// SQL server数据库标识
        /// </summary>
        public const string MSSQLAhChaFortunate = "MSSQLAhChaFortunate";

        /// <summary>
        /// 达梦数据库标识
        /// </summary>
        public const string DMAhChaFortunate = "DMAhChaFortunate";
    }
}
