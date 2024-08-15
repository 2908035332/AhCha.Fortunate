using SqlSugar;
namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysDictData")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysDictData
    {
        public SysDictData()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long TypeId { get; set; }

        /// <summary>
        /// Desc:字典名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:字典值1
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Value1 { get; set; }

        /// <summary>
        /// Desc:字典值2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Value2 { get; set; }

        /// <summary>
        /// Desc:字典值3
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Value3 { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sort { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:创建人id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? CreateUserId { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:修改人id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UpdateUserId { get; set; }

    }
}
