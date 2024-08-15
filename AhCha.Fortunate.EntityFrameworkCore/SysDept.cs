namespace AhCha.Fortunate.EntityFrameworkCore;

/// <summary>
/// 部门
/// </summary>
public partial class SysDept
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父级部门ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 部门名称
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
