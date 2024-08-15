using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace AhCha.Fortunate.Entity.MySQL
{
    ///<summary>
    ///
    ///</summary>
    
    [SugarTable("api_request_log")]
    [TenantAttribute(ConstConfigId.MySqlAhChaFortunate)]
    public partial class ApiRequestLog
    {
           public ApiRequestLog(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="Id")]
           public long Id {get;set;}

           /// <summary>
           /// Desc:控制器名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="ControllerName")]           
           public string? ControllerName {get;set;}

           /// <summary>
           /// Desc:方法名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="ActionName")]           
           public string? ActionName {get;set;}

           /// <summary>
           /// Desc:请求的参数JSON
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="Param")]           
           public string? Param {get;set;}

           /// <summary>
           /// Desc:请求时间
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="CreateTime")]           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:ip地址
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="Ip")]           
           public string? Ip {get;set;}

           /// <summary>
           /// Desc:请求方式
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="Method")]           
           public string? Method {get;set;}

           /// <summary>
           /// Desc:主机
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="Host")]           
           public string? Host {get;set;}

           /// <summary>
           /// Desc:url
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="Path")]           
           public string? Path {get;set;}

           /// <summary>
           /// Desc:创建人id
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="CreateUserId")]           
           public long? CreateUserId {get;set;}

    }
}
