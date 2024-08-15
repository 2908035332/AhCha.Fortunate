using Microsoft.EntityFrameworkCore;
namespace AhCha.Fortunate.EntityFrameworkCore;

public partial class AhChaFortunateContext : DbContext
{
    public AhChaFortunateContext()
    {
    }

    public AhChaFortunateContext(DbContextOptions<AhChaFortunateContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SysDept> SysDepts { get; set; }

    public virtual DbSet<SysDictDatum> SysDictData { get; set; }

    public virtual DbSet<SysDictType> SysDictTypes { get; set; }

    public virtual DbSet<SysLoginLog> SysLoginLogs { get; set; }

    public virtual DbSet<SysMenu> SysMenus { get; set; }

    public virtual DbSet<SysNotice> SysNotices { get; set; }

    public virtual DbSet<SysRole> SysRoles { get; set; }

    public virtual DbSet<SysRoleMenu> SysRoleMenus { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    public virtual DbSet<SysUserRole> SysUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AhChaFortunate;User ID=sa;Password=123;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SysDept>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysDept__3214EC07D4770CF3");

            entity.ToTable("SysDept", tb => tb.HasComment("部门"));

            entity.HasIndex(e => e.Name, "ix01").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人id");
            entity.Property(e => e.Desc)
                .HasMaxLength(50)
                .HasComment("部门描述");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasComment("部门名称");
            entity.Property(e => e.ParentId).HasComment("父级部门ID");
            entity.Property(e => e.Status).HasComment("是否启用");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人id");
        });

        modelBuilder.Entity<SysDictDatum>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("数据字典数据"));

            entity.HasIndex(e => new { e.TypeId, e.Name }, "ix1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("字典名称");
            entity.Property(e => e.Sort).HasComment("顺序");
            entity.Property(e => e.Status).HasComment("是否启用");
            entity.Property(e => e.TypeId).HasComment("类型ID");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人id");
            entity.Property(e => e.Value1)
                .HasMaxLength(200)
                .HasComment("字典值1");
            entity.Property(e => e.Value2)
                .HasMaxLength(200)
                .HasComment("字典值2");
            entity.Property(e => e.Value3)
                .HasMaxLength(200)
                .HasComment("字典值3");
        });

        modelBuilder.Entity<SysDictType>(entity =>
        {
            entity.ToTable("SysDictType", tb => tb.HasComment("数据字典类型"));

            entity.HasIndex(e => e.Name, "ix1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("字典代码");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人id");
            entity.Property(e => e.Desc)
                .HasMaxLength(50)
                .HasComment("字典类型描述");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasComment("字典类型名称");
            entity.Property(e => e.Status).HasComment("是否启用");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人id");
        });

        modelBuilder.Entity<SysLoginLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysLogin__3214EC073689353C");

            entity.ToTable("SysLoginLog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.Browser)
                .HasMaxLength(50)
                .HasComment("浏览器");
            entity.Property(e => e.DeptId).HasComment("部门ID");
            entity.Property(e => e.Device)
                .HasMaxLength(30)
                .HasComment("设备");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasComment("IP")
                .HasColumnName("IP");
            entity.Property(e => e.LoginTime)
                .HasComment("登录时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Os)
                .HasMaxLength(20)
                .HasComment("系统")
                .HasColumnName("OS");
            entity.Property(e => e.Uastr)
                .HasMaxLength(150)
                .HasComment("UA")
                .HasColumnName("UAStr");
            entity.Property(e => e.UserId).HasComment("用户ID");
        });

        modelBuilder.Entity<SysMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysMenu__3214EC077516A8FB");

            entity.ToTable("SysMenu");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.ApiTag)
                .HasMaxLength(20)
                .HasComment("访问的api控制器名称");
            entity.Property(e => e.Component)
                .HasMaxLength(100)
                .HasComment("组件路径");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人");
            entity.Property(e => e.Icon)
                .HasMaxLength(20)
                .HasComment("菜单图标");
            entity.Property(e => e.IsAffix).HasComment("是否固定(不允许关闭)");
            entity.Property(e => e.IsHide).HasComment("是否隐藏");
            entity.Property(e => e.IsIframe).HasComment("是否内嵌网址");
            entity.Property(e => e.IsKeepAlive).HasComment("是否缓存");
            entity.Property(e => e.IsLink).HasComment("是否外部连接(新标签打开)");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasComment("路由名称");
            entity.Property(e => e.ParentId).HasComment("父级ID");
            entity.Property(e => e.Path)
                .HasMaxLength(100)
                .HasComment("路由地址");
            entity.Property(e => e.Permission)
                .HasMaxLength(200)
                .HasComment("操作权限");
            entity.Property(e => e.Redirect)
                .HasMaxLength(200)
                .HasComment("重定向地址");
            entity.Property(e => e.Sort).HasComment("排序");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasComment("菜单标题");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasComment("菜单类型");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasComment("外部或者Iframe网址");
        });

        modelBuilder.Entity<SysNotice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysNotic__3214EC07EBD02889");

            entity.ToTable("SysNotice");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasComment("描述");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasComment("是否启用");
            entity.Property(e => e.Title)
                .HasMaxLength(60)
                .HasComment("角色名称");
            entity.Property(e => e.Type)
                .HasMaxLength(2)
                .HasComment("类型(10公告 20通知)");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<SysRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysRole__3214EC07DE4776D7");

            entity.ToTable("SysRole", tb => tb.HasComment("角色表"));

            entity.HasIndex(e => e.Name, "ix01").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人id");
            entity.Property(e => e.DataRang)
                .HasMaxLength(20)
                .HasComment("数据范围");
            entity.Property(e => e.Desc)
                .HasMaxLength(50)
                .HasComment("部门描述");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasComment("角色名称");
            entity.Property(e => e.Permission)
                .HasMaxLength(200)
                .HasComment("数据权限(当范围为自定义时选择的部门)");
            entity.Property(e => e.Status).HasComment("是否启用");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人id");
        });

        modelBuilder.Entity<SysRoleMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysRoleM__3214EC074025A52F");

            entity.ToTable("SysRoleMenu");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.MenuId).HasComment("菜单ID");
            entity.Property(e => e.RoleId).HasComment("角色ID");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SysUser__3214EC07188ADB1B");

            entity.ToTable("SysUser");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.Account)
                .HasMaxLength(100)
                .HasComment("账号");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasComment("地址");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasComment("创建人id");
            entity.Property(e => e.IdCard)
                .HasMaxLength(20)
                .HasComment("身份证");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("名称");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasComment("密码");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasComment("手机号码");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .HasComment("备注");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .HasComment("密码加盐码");
            entity.Property(e => e.UpdateTime)
                .HasComment("修改时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasComment("修改人id");
            entity.Property(e => e.UserState).HasComment("状态");
        });

        modelBuilder.Entity<SysUserRole>(entity =>
        {
            entity.ToTable("SysUserRole");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RoleId).HasComment("角色id");
            entity.Property(e => e.UserId).HasComment("用户id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
