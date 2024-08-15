
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysUsersDto
{
    public class SysUserOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Phone { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UserState { get; set; }
        public string Remark { get; set; }
    }


    public class UserTreeMenuOutput
    {
        public string path { get; set; }
        public string name { get; set; }
        public string component { get; set; }
        public string redirect { get; set; }
        public MenuMeta meta { get; set; }
        public List<UserTreeMenuOutput> children { get; set; }
    }

    public class MenuMeta
    {
        public string title { get; set; }
        public string icon { get; set; }
        public bool isHide { get; set; }
        public bool isKeepAlive { get; set; }
        public bool isAffix { get; set; }
        public bool isIframe { get; set; }
        public bool isLink { get; set; }
        public string url { get; set; }
    }

    public class UserMenu
    {
        public long userId { get; set; }
        public long deptId { get; set; }
        public long roleId { get; set; }
        public long menuId { get; set; }
        public long menuParentId { get; set; }
        public string menuTitle { get; set; }
        public string menuName { get; set; }
        public string menuPath { get; set; }
        public string menuIcon { get; set; }
        public string menuComponent { get; set; }
        public string menuRedirect { get; set; }
        public bool menuIsHide { get; set; }
        public bool menuIsIframe { get; set; }
        public bool menuIsKeepAlive { get; set; }
        public bool menuIsLink { get; set; }
        public bool menuIsAffix { get; set; }
        public string menuLinkUrl { get; set; }
    }

    public class UserButton
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }


    public class LoginUserInfo
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string IP { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Browser { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HolidaysModel
    {
        public string holidayDate { get; set; }
        public int holidayFlag { get; set; } = 1;
        public string Week { get; set; }
    }
}
