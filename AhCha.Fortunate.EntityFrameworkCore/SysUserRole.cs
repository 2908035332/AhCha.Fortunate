
namespace AhCha.Fortunate.EntityFrameworkCore;

public partial class SysUserRole
{
    public long Id { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 角色id
    /// </summary>
    public long RoleId { get; set; }
}
