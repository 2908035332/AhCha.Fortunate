
namespace AhCha.Fortunate.Common.Utility
{
    public class GenerateCoreUtil
    {
        public static string ConvertDataType(string dataType)
        {
            switch (dataType)
            {
                case "text":
                case "varchar":
                case "char":
                case "nvarchar":
                case "nchar":
                case "timestamp":
                    return "string";

                case "int":
                    return "int";

                case "smallint":
                    return "Int16";

                case "tinyint":
                    return "byte";

                case "bigint":
                case "integer"://sqlite数据库
                    return "long";

                case "bit":
                    return "bool";

                case "money":
                case "smallmoney":
                case "numeric":
                case "decimal":
                    return "decimal";

                case "real":
                    return "Single";

                case "datetime":
                case "smalldatetime":
                    return "DateTime";

                case "float":
                    return "double";

                case "image":
                case "binary":
                case "varbinary":
                    return "byte[]";

                case "uniqueidentifier":
                    return "Guid";

                default:
                    return "object";
            }
        }
        public static string DataTypeToEff(string dataType)
        {
            if (string.IsNullOrEmpty(dataType)) return "";
            return dataType switch
            {
                "string" => "input",
                "int" => "number",
                "long" => "input",
                "float" => "input",
                "double" => "input",
                "decimal" => "input",
                "bool" => "input",
                "Guid" => "input",
                "DateTime" => "input",
                _ => "input",
            };
        }
        public static bool IsCommonColumn(string columnName)
        {
            var columnList = new List<string>()
            {
                "CreatedTime", "UpdatedTime", "CreatedUserId", "CreatedUserName", "UpdatedUserId", "UpdatedUserName", "IsDeleted"
            };
            return columnList.Contains(columnName);
        }
    }
}
