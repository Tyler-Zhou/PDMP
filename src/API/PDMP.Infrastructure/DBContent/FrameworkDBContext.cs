/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-22 1:28:10
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using Microsoft.EntityFrameworkCore;
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 框架数据库库上下文
    /// </summary>
    public class FrameworkDBContext : DbContext
    {
        #region 成员(Member)
        /// <summary>
        /// 权限集
        /// </summary>
        public DbSet<PermissionEntity> Permissions { get; set; }
        /// <summary>
        /// 角色集
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }
        /// <summary>
        /// 用户集
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// 菜单集
        /// </summary>
        public DbSet<MenuItemEntity> MenuItems { get; set; }
        
        /// <summary>
        /// 角色权限集
        /// </summary>
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }
        
        /// <summary>
        /// 用户角色集
        /// </summary>
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        /// <summary>
        /// 系统版本集
        /// </summary>
        public DbSet<VersionEntity> Versions { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <param name="options"></param>
        public FrameworkDBContext(DbContextOptions<FrameworkDBContext> options) : base(options)
        {

        }
        #endregion

        #region 重写方法(Override Method)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //RolePermissions，通过实现两个1:n，实现m:n
            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(p => p.Role)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(k => k.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(p => p.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(k => k.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //UserRoles，通过实现两个1:n，实现m:n
            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(p => p.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(k => k.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //菜单表树状结构
            modelBuilder.Entity<MenuItemEntity>()
                //主语this，拥有Children
                .HasMany(x => x.Children)
                //主语Children，每个Child拥有一个Parent
                .WithOne(x => x.Parent)
                //主语Children，每个Child的外键是ParentId
                .HasForeignKey(x => x.ParentId)
                //这里必须是非强制关联，否则报错：Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                .OnDelete(DeleteBehavior.ClientSetNull);
        } 
        #endregion
    }
}

