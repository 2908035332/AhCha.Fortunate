using SqlSugar;
using System.Text;
using System.Reflection;
using AhCha.Fortunate.Common;
using AhCha.Fortunate.IService;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.Common.Extensions;
using AhCha.Fortunate.Repositories.SqlSugar;

namespace AhCha.Fortunate.Service
{
    public class GenerateCoreService : IGenerateCoreService
    {

        /// <summary>
        /// 获取系统数据库
        /// </summary>
        /// <returns></returns>
        public Task<List<SelectHelper>> GetSysDatabase()
        {
            var data = AhChaFortunateGlobalContext.DatabaseConfigs.Select(entity => new SelectHelper()
            {
                label = entity.ConnectionName,
                value = entity.ConfigId,
            }).ToList();
            return Task.FromResult(data);
        }

        /// <summary>
        /// 根据数据库获取所有表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<SelectHelper>> GetSysTable(GenerateCoreInput input)
        {
            var entity = AhChaFortunateGlobalContext.DatabaseConfigs.Where(x => x.ConfigId == input.ConfigId).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("请先配置数据库连接字符串");
            }
            if (entity.DbType.ToEnum<DbType>() == DbType.Dm)
            {
                throw new Exception("达梦数据库暂且未配置好");
            }
            using (var db = SqlSugarContext.GetInstance(entity))
            {
                try
                {
                    var tables = db.DbMaintenance.GetTableInfoList().Select(x => new SelectHelper()
                    {
                        value = x.Name,
                        label = x.Name,
                    }).ToList();
                    return Task.FromResult(tables);
                }
                catch (Exception ex)
                {
                    throw new Exception($"{entity.ConnectionName}数据库不存在或密码错误");
                }
            }
        }

        /// <summary>
        /// 生成后端代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<string> GeneratecCore(GenerateCoreInput input)
        {
            //存放路径
            string BasePath = Environment.CurrentDirectory.Replace("Api", string.Empty);
            //驼峰
            GenerateCore.SetName = input.TableName.ConvertToHump();
            var entity = AhChaFortunateGlobalContext.DatabaseConfigs.Where(x => x.ConfigId == input.ConfigId).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("请先配置数据库连接字符串");
            }
            if (entity.DbType.ToEnum<DbType>() == DbType.Dm)
            {
                throw new Exception("达梦数据库暂且未配置好");
            }
            string nameSpace = string.Empty;

            List<FieldInput> attributes = new List<FieldInput>();

            #region 生成数据库实体TEntity
            using (var db = SqlSugarContext.GetInstance(entity))
            {
                string TenantStr = string.Empty, directoryPath = string.Concat(BasePath, "Entity");
                switch (entity.DbType.ToEnum<DbType>())
                {
                    case DbType.MySql:
                        TenantStr = "\r\n    [TenantAttribute(ConstConfigId.MySqlAhChaFortunate)]";
                        nameSpace = "MySQL";
                        break;
                    case DbType.SqlServer:
                        TenantStr = "\r\n    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]";
                        nameSpace = "MSSQL";
                        break;
                    case DbType.Dm:
                        TenantStr = "\r\n    [TenantAttribute(ConstConfigId.DMAhChaFortunate)]";
                        nameSpace = "DaMeng";
                        break;
                    default:
                        throw new Exception($"{entity.DbType.ToEnum<DbType>().ToString()}数据库暂未支持");
                }

                db.DbFirst.Where(TableName => SqlFunc.Equals(TableName.ToLower(), input.TableName.ToLower()))
                    .FormatClassName(Name => Name.ConvertToHump())
                    .FormatPropertyName(PropName => PropName.ConvertToHump()).IsCreateAttribute().StringNullable()
                    .SettingClassDescriptionTemplate(it =>
                    {
                        return it + TenantStr;
                    })
                    .CreateClassFile(Path.Combine(directoryPath, nameSpace), string.Concat(GenerateCore.GetTEntityNameSpace, nameSpace));

                if (db.DbMaintenance.IsAnyTable(input.TableName))
                {
                    var columns = db.DbMaintenance.GetColumnInfosByTableName(input.TableName, false);
                    attributes = columns.Select(x => new FieldInput()
                    {
                        PropName = x.DbColumnName.ConvertToHump(),
                        IsNull = x.IsNullable,
                        PropType = x.DataType,
                        PropDescription = x.ColumnDescription,
                    }).ToList();
                }
            }
            #endregion

            if (attributes == null || attributes.Count <= 0)
            {
                throw new Exception($"{GenerateCore.SetName}表不存在，无法读取");
            }

            #region 获取字段名，数据类型，是否可空 --- 注解原因：（无法在运行时解析到实体，现改成从数据库中读取列明）2024-04-28
            /*
            var type = Assembly.Load(GenerateCore.GetTEntityNameSpace.TrimEnd('.')).GetTypes().Where(x => x.Name.ToLower() == GenerateCore.SetName.ToLower()).FirstOrDefault();
            PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (PropertyInfo PropertyInfo in propertyinfo)
            {
                bool IsNull = false;
                string PropType = string.Empty;
                //可空
                if (PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    IsNull = true;
                    Type[] typeArray = PropertyInfo.PropertyType.GetGenericArguments();
                    PropType = typeArray[0].Name.ToString();
                }
                else
                {
                    IsNull = false;
                    PropType = PropertyInfo.GetMethod.ReturnType.Name.ToString();
                }

                attributes.Add(new FieldInput()
                {
                    IsNull = IsNull,
                    PropType = PropType,
                    PropName = PropertyInfo.Name,
                });
            }
            */
            #endregion

            Assembly assembly = Assembly.GetExecutingAssembly();

            #region 读取TEntityDtoTemplate模板，替换内容得到（Input 和 Output）字符，生成Dto
            StringBuilder TEntityDtoTemplateBuilder = new StringBuilder();
            foreach (FieldInput attribute in attributes)
            {
                string IsNull = attribute.IsNull ? "?" : string.Empty;
                TEntityDtoTemplateBuilder.Append("\r\n        /// <summary>");
                TEntityDtoTemplateBuilder.Append($"\r\n        /// {attribute.PropDescription}");
                TEntityDtoTemplateBuilder.Append("\r\n        /// </summary>");
                TEntityDtoTemplateBuilder.Append($"\r\n        public {attribute.PropType.ConvertToDataType() + IsNull} {attribute.PropName} {{ get; set; }}");
                TEntityDtoTemplateBuilder.Append("\r\n");
            }

            #region Input

            string? TEntityDtoInputTemplate = assembly.GetManifestResourceNames().FirstOrDefault(TemplateName => TemplateName.Contains(GenerateCore.TEntityInputTemplate));
            Stream? TEntityDtoInputTemplateFile = assembly.GetManifestResourceStream(TEntityDtoInputTemplate);
            string? TEntityDtoInputTemplateStream = new StreamReader(TEntityDtoInputTemplateFile).ReadToEnd();

            string TEntityDtoInput = TEntityDtoInputTemplateStream.Replace("{namespaceStr}", string.Concat(GenerateCore.GetITEntityDtoNameSpace, nameSpace, ".", GenerateCore.GetDtoName))
                  .Replace("{EntityInput}", string.Concat(GenerateCore.SetName, "Input"))
                   .Replace("{QueryEntityDto}", string.Concat("Query", GenerateCore.SetName, "Input"))
                   .Replace("{AddEntityInput}", string.Concat("Post", GenerateCore.SetName, "Input"))
                   .Replace("{PutEntityInput}", string.Concat("Put", GenerateCore.SetName, "Input"))
                   .Replace("{DeleteEntityInput}", string.Concat("Delete", GenerateCore.SetName, "Input"))
                   .Replace("{Attribute}", TEntityDtoTemplateBuilder.ToString());
            #endregion


            #region Output

            string? TEntityDtoTemplate = assembly.GetManifestResourceNames().FirstOrDefault(TemplateName => TemplateName.Contains(GenerateCore.TEntityOutputTemplate));
            Stream? TEntityDtoTemplateFile = assembly.GetManifestResourceStream(TEntityDtoTemplate);
            string? TEntityDtoTemplateStream = new StreamReader(TEntityDtoTemplateFile).ReadToEnd();

            string TEntityDtoOutput = TEntityDtoTemplateStream.Replace("{namespaceStr}", string.Concat(GenerateCore.GetITEntityDtoNameSpace, nameSpace, ".", GenerateCore.GetDtoName))
                 .Replace("{EntityOutput}", string.Concat(GenerateCore.SetName, "Output"))
                 .Replace("{Attribute}", TEntityDtoTemplateBuilder.ToString());

            #endregion

            string TEntityDtoPath = Path.Combine(string.Concat(BasePath, "ModelsDto"), nameSpace);
            FileUtil.CreateDirectory(Path.Combine(TEntityDtoPath, GenerateCore.GetDtoName));
            FileUtil.CreateFile(Path.Combine(TEntityDtoPath, GenerateCore.GetDtoName, GenerateCore.GetDtoInputName), Encoding.Default.GetBytes(TEntityDtoInput));
            FileUtil.CreateFile(Path.Combine(TEntityDtoPath, GenerateCore.GetDtoName, GenerateCore.GetDtoOutputName), Encoding.Default.GetBytes(TEntityDtoOutput));
            #endregion

            #region 读取TEntityIServiceTemplate模板，替换内容得到I<TEntity>Service字符，生成I<TEntity>Service
            string? ITEntityServiceTemplate = assembly.GetManifestResourceNames().FirstOrDefault(TemplateName => TemplateName.Contains(GenerateCore.TEntityIServiceTemplate));
            Stream? ITEntityServiceTemplateFile = assembly.GetManifestResourceStream(ITEntityServiceTemplate);
            string? ITEntityServiceTemplateStream = new StreamReader(ITEntityServiceTemplateFile).ReadToEnd();
            StringBuilder ITEntityServiceTemplateBuilder = new StringBuilder();
            string IServiceUsing = $"using AhCha.Fortunate.ModelsDto;\r\nusing {string.Concat(GenerateCore.GetTEntityNameSpace, nameSpace)};\r\nusing {string.Concat(GenerateCore.GetITEntityDtoNameSpace, nameSpace, ".", GenerateCore.GetDtoName)};";

            foreach (string method in GenerateCore.GetMethods)
            {
                switch (method)
                {
                    case "Page":
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        /// 分页获取{GenerateCore.SetName}数据");
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        Task<SqlSugarPagedList<{GenerateCore.SetName}Output>> Get{GenerateCore.SetName + method}(Query{GenerateCore.SetName}Input input);");
                        ITEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Post":
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        /// 新增{GenerateCore.SetName}表数据");
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input);");
                        ITEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Put":
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        /// 修改{GenerateCore.SetName}表数据");
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input);");
                        ITEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Delete":
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        /// 删除{GenerateCore.SetName}表数据");
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input);");
                        ITEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Detail":
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        /// 获取{GenerateCore.SetName}详情数据");
                        ITEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        ITEntityServiceTemplateBuilder.Append($"\r\n        Task<{GenerateCore.SetName}Output> Get{GenerateCore.SetName}Detail({GenerateCore.SetName}Input input);");
                        ITEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    default:
                        break;
                }
            }

            string ITEntityService = ITEntityServiceTemplateStream.Replace("{usingStr}", IServiceUsing)
                  .Replace("{namespaceStr}", string.Concat(GenerateCore.GetITEntityServiceNameSpace, nameSpace))
                  .Replace("{TEntity}", GenerateCore.SetName)
                  .Replace("{interfaces}", ITEntityServiceTemplateBuilder.ToString());

            string IServicePath = Path.Combine(string.Concat(BasePath, "IService"), nameSpace);
            FileUtil.CreateDirectory(Path.Combine(IServicePath, GenerateCore.SetName));
            FileUtil.CreateFile(Path.Combine(IServicePath, GenerateCore.GetIServiceName), Encoding.Default.GetBytes(ITEntityService));
            #endregion

            #region 读取TEntityServiceTemplate模板，替换<TEntity>ServiceTemplate字符，生成<TEntity>Service
            string? TEntityServiceTemplate = assembly.GetManifestResourceNames().FirstOrDefault(TemplateName => TemplateName.Contains(GenerateCore.TEntityServiceTemplate));
            Stream? TEntityServiceTemplateFile = assembly.GetManifestResourceStream(TEntityServiceTemplate);
            string? TEntityServiceTemplateStream = new StreamReader(TEntityServiceTemplateFile).ReadToEnd();
            StringBuilder TEntityServiceTemplateBuilder = new StringBuilder();

            string ServiceUsing = $"\r\nusing {string.Concat(GenerateCore.GetTEntityNameSpace, nameSpace)};\r\nusing {string.Concat(GenerateCore.GetITEntityServiceNameSpace, nameSpace)};\r\nusing {string.Concat(GenerateCore.GetITEntityDtoNameSpace, nameSpace, ".", GenerateCore.GetDtoName)};";
            foreach (string method in GenerateCore.GetMethods)
            {
                switch (method)
                {
                    case "Page":
                        TEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        /// 分页获取{GenerateCore.SetName}数据");
                        TEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        public async Task<SqlSugarPagedList<{GenerateCore.SetName}Output>> Get{GenerateCore.SetName + method}(Query{GenerateCore.SetName}Input input)");
                        TEntityServiceTemplateBuilder.Append("\r\n        {");
                        TEntityServiceTemplateBuilder.Append("\r\n            var query = await _TEntityRep.AsQueryable().OrderByDescending(x => x.CreateTime)");
                        TEntityServiceTemplateBuilder.Append($"\r\n                .Select<{GenerateCore.SetName}Output>()");
                        TEntityServiceTemplateBuilder.Append("\r\n                .ToPagedListAsync(input.PageIndex, input.PageSize);");
                        TEntityServiceTemplateBuilder.Append("\r\n            return query;");
                        TEntityServiceTemplateBuilder.Append("\r\n        }");
                        TEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Post":
                        TEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        /// 新增{GenerateCore.SetName}数据");
                        TEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        public async Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input)");
                        TEntityServiceTemplateBuilder.Append("\r\n        {");
                        TEntityServiceTemplateBuilder.Append($"\r\n            var entity = input.Adapt<{GenerateCore.SetName}>();");
                        TEntityServiceTemplateBuilder.Append("\r\n            return await _TEntityRep.InsertAsync(entity) > 0;");
                        TEntityServiceTemplateBuilder.Append("\r\n        }");
                        TEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Put":
                        TEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        /// 修改{GenerateCore.SetName}数据");
                        TEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        public async Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input)");
                        TEntityServiceTemplateBuilder.Append("\r\n        {");
                        TEntityServiceTemplateBuilder.Append($"\r\n            var entity = input.Adapt<{GenerateCore.SetName}>();");
                        TEntityServiceTemplateBuilder.Append("\r\n            return await _TEntityRep.UpdateIgnoreNullAsync(entity) > 0;");
                        TEntityServiceTemplateBuilder.Append("\r\n        }");
                        TEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Delete":
                        TEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        /// 删除{GenerateCore.SetName}数据（物理删除）");
                        TEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        public async Task<bool> {method}{GenerateCore.SetName}({method}{GenerateCore.SetName}Input input)");
                        TEntityServiceTemplateBuilder.Append("\r\n        {");
                        TEntityServiceTemplateBuilder.Append("\r\n            int Execute = await _TEntityRep.DeleteAsync(x => SqlFunc.Equals(x.Id,input.Id));");
                        TEntityServiceTemplateBuilder.Append("\r\n            return Execute > 0;");
                        TEntityServiceTemplateBuilder.Append("\r\n        }");
                        TEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    case "Detail":
                        TEntityServiceTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        /// 获取{GenerateCore.SetName}详情数据");
                        TEntityServiceTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityServiceTemplateBuilder.Append($"\r\n        public async Task<{GenerateCore.SetName}Output> Get{GenerateCore.SetName + method}({GenerateCore.SetName}Input input)");
                        TEntityServiceTemplateBuilder.Append("\r\n        {");
                        TEntityServiceTemplateBuilder.Append("\r\n            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));");
                        TEntityServiceTemplateBuilder.Append($"\r\n            return entity.Adapt<{GenerateCore.SetName}Output>();");
                        TEntityServiceTemplateBuilder.Append("\r\n        }");
                        TEntityServiceTemplateBuilder.Append("\r\n");
                        break;
                    default:
                        break;
                }
            }

            string TEntityService = TEntityServiceTemplateStream
                .Replace("{namespaceStr}", string.Concat(GenerateCore.GetTEntityServiceNameSpace, nameSpace))
                .Replace("{usingStr}", ServiceUsing)
                .Replace("{TEntity}", GenerateCore.SetName)
                .Replace("{ImplementInterface}", TEntityServiceTemplateBuilder.ToString());

            string ServicePath = Path.Combine(string.Concat(BasePath, "Service"), nameSpace);
            FileUtil.CreateDirectory(Path.Combine(ServicePath, GenerateCore.SetName));
            FileUtil.CreateFile(Path.Combine(ServicePath, GenerateCore.GetServiceName), Encoding.Default.GetBytes(TEntityService));
            #endregion

            #region 读取TEntityControllerTemplate模板，替换<TEntity>ControllerTemplate字符，生成<TEntity>Controller

            string? TEntityControllerTemplate = assembly.GetManifestResourceNames().FirstOrDefault(TemplateName => TemplateName.Contains(GenerateCore.TEntityControllerTemplate));
            Stream? TEntityControllerTemplateFile = assembly.GetManifestResourceStream(TEntityControllerTemplate);
            string? TEntityControllerTemplateStream = new StreamReader(TEntityControllerTemplateFile).ReadToEnd();
            StringBuilder TEntityControllerTemplateBuilder = new StringBuilder();

            foreach (string method in GenerateCore.GetMethods)
            {
                switch (method)
                {
                    case "Page":
                        TEntityControllerTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        /// 分页获取{GenerateCore.SetName}数据");
                        TEntityControllerTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityControllerTemplateBuilder.Append("\r\n        [HttpGet]");
                        TEntityControllerTemplateBuilder.Append($"\r\n        public async Task<SqlSugarPagedList<{GenerateCore.SetName}Output>> Get{GenerateCore.SetName}Page([FromQuery] Query{GenerateCore.SetName}Input input)");
                        TEntityControllerTemplateBuilder.Append("\r\n        {");
                        TEntityControllerTemplateBuilder.Append($"\r\n            return await _i{GenerateCore.SetName}Service.Get{GenerateCore.SetName}Page(input);");
                        TEntityControllerTemplateBuilder.Append("\r\n        }");
                        TEntityControllerTemplateBuilder.Append("\r\n");
                        break;
                    case "Detail":
                        TEntityControllerTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        /// 获取{GenerateCore.SetName}表详情数据");
                        TEntityControllerTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityControllerTemplateBuilder.Append("\r\n        [HttpGet]");
                        TEntityControllerTemplateBuilder.Append($"\r\n        public async Task<{GenerateCore.SetName}Output> Get{GenerateCore.SetName}Detail([FromQuery] {GenerateCore.SetName}Input input)");
                        TEntityControllerTemplateBuilder.Append("\r\n        {");
                        TEntityControllerTemplateBuilder.Append($"\r\n            return await _i{GenerateCore.SetName}Service.Get{GenerateCore.SetName}Detail(input);");
                        TEntityControllerTemplateBuilder.Append("\r\n        }");
                        TEntityControllerTemplateBuilder.Append("\r\n");
                        break;
                    case "Post":
                        TEntityControllerTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        /// 新增{GenerateCore.SetName}表数据");
                        TEntityControllerTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        [HttpPost]");
                        TEntityControllerTemplateBuilder.Append($"\r\n        public async Task<bool> {method + GenerateCore.SetName}({method + GenerateCore.SetName}Input input)");
                        TEntityControllerTemplateBuilder.Append("\r\n        {");
                        TEntityControllerTemplateBuilder.Append($"\r\n            return await _i{GenerateCore.SetName}Service.{method + GenerateCore.SetName}(input);");
                        TEntityControllerTemplateBuilder.Append("\r\n        }");
                        TEntityControllerTemplateBuilder.Append("\r\n");
                        break;
                    case "Put":
                        TEntityControllerTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        /// 修改{GenerateCore.SetName}表数据");
                        TEntityControllerTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        [HttpPut]");
                        TEntityControllerTemplateBuilder.Append($"\r\n        public async Task<bool> {method + GenerateCore.SetName}({method + GenerateCore.SetName}Input input)");
                        TEntityControllerTemplateBuilder.Append("\r\n        {");
                        TEntityControllerTemplateBuilder.Append($"\r\n            return await _i{GenerateCore.SetName}Service.{method + GenerateCore.SetName}(input);");
                        TEntityControllerTemplateBuilder.Append("\r\n        }");
                        TEntityControllerTemplateBuilder.Append("\r\n");
                        break;
                    case "Delete":
                        TEntityControllerTemplateBuilder.Append("\r\n        /// <summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        /// 删除{GenerateCore.SetName}表数据（物理删除）");
                        TEntityControllerTemplateBuilder.Append("\r\n        /// </summary>");
                        TEntityControllerTemplateBuilder.Append($"\r\n        [HttpDelete]");
                        TEntityControllerTemplateBuilder.Append($"\r\n        public async Task<bool> {method + GenerateCore.SetName}({method + GenerateCore.SetName}Input input)");
                        TEntityControllerTemplateBuilder.Append("\r\n        {");
                        TEntityControllerTemplateBuilder.Append($"\r\n            return await _i{GenerateCore.SetName}Service.{method + GenerateCore.SetName}(input);");
                        TEntityControllerTemplateBuilder.Append("\r\n        }");
                        TEntityControllerTemplateBuilder.Append("\r\n");
                        break;
                    default:
                        break;
                }
            }
            string ControllerUsing = $"\r\nusing {string.Concat(GenerateCore.GetITEntityDtoNameSpace, nameSpace, ".", GenerateCore.GetDtoName)};\r\nusing {string.Concat(GenerateCore.GetITEntityServiceNameSpace, nameSpace)};";

            string TEntityController = TEntityControllerTemplateStream
               .Replace("{usingStr}", ControllerUsing)
               .Replace("{namespaceStr}", string.Concat(GenerateCore.GetTEntityControllerNameSpace, nameSpace))
               .Replace("{TEntity}", GenerateCore.SetName)
               .Replace("{ApiList}", TEntityControllerTemplateBuilder.ToString());

            string ControllerPath = Path.Combine(string.Concat(BasePath, "Api"), "Controllers", nameSpace);
            FileUtil.CreateDirectory(Path.Combine(ControllerPath, GenerateCore.SetName));
            FileUtil.CreateFile(Path.Combine(ControllerPath, GenerateCore.GetControllerName), Encoding.Default.GetBytes(TEntityController));
            #endregion

            return Task.FromResult("生成成功");
        }


    }
}
