/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:11:50
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Microsoft.EntityFrameworkCore;
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 库数据库上下文
    /// </summary>
    public class LibraryDBContext : DbContext
    {
        #region 成员(Member)
        /// <summary>
        /// 作者集
        /// </summary>
        public DbSet<AuthorEntity> Authors { get; set; }
        /// <summary>
        /// 书籍集
        /// </summary>
        public DbSet<BookEntity> Books { get; set; }
        /// <summary>
        /// 章节集
        /// </summary>
        public DbSet<ChapterEntity> Chapters { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 库数据库上下文
        /// </summary>
        /// <param name="options"></param>
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
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
            //Books
            modelBuilder.Entity<BookEntity>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(k => k.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Chapters
            modelBuilder.Entity<ChapterEntity>()
                .HasOne(p => p.Book)
                .WithMany(p => p.Chapters)
                .HasForeignKey(k => k.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
        #endregion

    }
}

