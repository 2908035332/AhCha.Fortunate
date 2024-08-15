
namespace AhCha.Fortunate.Common.Global
{
    public class DatabaseConfig
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public String ConfigId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String ConnectionName { get; set; }

        /// <summary>
        /// 数据库类型
        /// MySql=0,SqlServer=1,Sqlite=2,Oracle=3,PostgreSQL=4,Dm=5,Kdbndp=6,Oscar=7,MySqlConnector=8,Access=9,OpenGauss=10,QuestDB=11,HG=12,ClickHouse=13,GBase=14,Odbc=0xF,OceanBaseForOracle=0x10,TDengine=17,GaussDB=18,OceanBase=19,Tidb=20,Custom=900
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary>
        /// 是否自动关闭连接
        /// </summary>
        public Boolean IsAutoCloseConnection { get; set; }
    }
}
