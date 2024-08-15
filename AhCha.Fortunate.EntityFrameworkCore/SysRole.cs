
namespace AhCha.Fortunate.EntityFrameworkCore;

/// <summary>
/// 角色表
/// </summary>
public partial class SysRole
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 部门描述
    /// </summary>
    public string? Desc { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Status { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public string? DataRang { get; set; }

    /// <summary>
    /// 数据权限(当范围为自定义时选择的部门)
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人id
    /// </summary>
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 修改人id
    /// </summary>
    public long? UpdateUserId { get; set; }
}
