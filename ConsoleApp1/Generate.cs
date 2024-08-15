using SqlSugar;
using AhCha.Fortunate.Common.Extensions;

namespace ConsoleApp1
{
    public class Generate
    {
        private static string directoryPath = "D:\\非系统文件夹\\Entity\\";

        /// <summary>
        /// 生成实体（根据表名生成）
        /// </summary>
        /// <param name="TableNameInput"></param>
        public static void GenerateDbFirst(string TableNameInput)
        {
            ConnectionConfig connectionConfig = new ConnectionConfig()
            {
                DbType = DbType.MySql,//数据库类型
                ConnectionString = "Server=111.230.31.22;uid=root;pwd=QING69qing..;database=ahchafortunate;port=3306;",
                IsAutoCloseConnection = true,//是否自动关闭连接
                InitKeyType = InitKeyType.Attribute
            };
            using (var db = new SqlSugarClient(connectionConfig))
            {
                db.DbFirst.Where(TableName => SqlFunc.Equals(TableName.ToLower(), TableNameInput.ToLower()))
                    //格式话Class名称
                    .FormatClassName(Name => Name.ConvertToHump())
                    //格式化属性名称，防止下划线___
                    .FormatPropertyName(PropName => PropName.ConvertToHump()).IsCreateAttribute().StringNullable()
                    .SettingClassDescriptionTemplate(it =>
                    {
                        return GetTenantAttribute(connectionConfig.DbType);
                    })
                    .CreateClassFile(Path.Combine(directoryPath, "Entity"), "Entity");
            }
        }

        /// <summary>
        /// 生成实体（根据表名生成）
        /// </summary>
        /// <param name="TableNameInputs"></param>
        public static void GenerateDbFirst(string[] TableNameInputs)
        {
            var TableNames = new List<string>();
            TableNameInputs.ToList().ForEach(x => TableNames.Add(x.ToLower()));
            ConnectionConfig connectionConfig = new ConnectionConfig()
            {
                DbType = DbType.MySql,//数据库类型
                ConnectionString = "Server=111.230.31.22;uid=root;pwd=QING69qing..;database=ahchafortunate;port=3306;",
                IsAutoCloseConnection = true,//是否自动关闭连接
                InitKeyType = InitKeyType.Attribute
            };
            using (var db = new SqlSugarClient(connectionConfig))
            {
                db.DbFirst.Where(TableName => TableNames.Contains(TableName.ToLower()))
                  .FormatClassName(Name => Name.ConvertToHump())
                  .FormatPropertyName(PropName => PropName.ConvertToHump()).IsCreateAttribute().StringNullable()
                  .SettingClassDescriptionTemplate(it =>
                  {
                      return GetTenantAttribute(connectionConfig.DbType);
                  })
                  .CreateClassFile(Path.Combine(directoryPath, "Entity"), "Entity");
            }
        }

        /// <summary>
        /// 数据库标识
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetTenantAttribute(DbType type)
        {
            string TenantStr = string.Empty;
            switch (type)
            {
                case DbType.MySql:
                    TenantStr = "\r\n    [TenantAttribute(ConstConfigId.MySqlAhChaFortunate)]";
                    break;
                case DbType.SqlServer:
                    TenantStr = "\r\n    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]";
                    break;
                case DbType.Dm:
                    TenantStr = "\r\n    [TenantAttribute(ConstConfigId.DMAhChaFortunate)]";
                    break;
            }
            return TenantStr;
        }
    }
}
