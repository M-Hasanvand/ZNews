using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Roles;
using ZNews.Domain.Entities.HomePage.Views;
using ZNews.Domain.Entities.Menues;
using ZNews.Domain.Entities.Newses;
using ZNews.Domain.Entities.Users;

namespace ZNews.Persistence.Context
{
    public class DataBaseContext:DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
                
        }

        //----Entities--------------------------
        public DbSet<User> Users { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsInTag> NewsInTags { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ChildMenu> ChildMenus { get; set; }
        public DbSet<ChildMenu_Category> ChildMenu_Categories { get; set; }
        public DbSet<ChildMenu_Tag> ChildMenu_Tags { get; set; }
        public DbSet<HomePageView> HomePageViews { get; set; }
        //---------ModelBinding---------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role() {Id=1,Name=nameof( UserRole.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role() {Id=2,Name= nameof(UserRole.Reporter)});


            ///را بلدم ولی در این پروژه خودم دستی رابطه را پاک کردم OnDelete همه گزینه های 
            modelBuilder.Entity<News>().HasOne(p => p.User).WithMany(p => p.News).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(p => p.User).WithMany(p => p.Comments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tag>().HasOne(p => p.User).WithMany(p=>p.Tags).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasOne(p => p.User).WithMany(p => p.Categories).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ChildMenu>().HasOne(p => p.User).WithMany(p => p.ChildMenus).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ChildMenu>().HasOne(p => p.Menu).WithMany(p => p.Children).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.NationalNumber).IsUnique();

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemove);

            modelBuilder.Entity<News>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<Tag>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<Comment>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<NewsInTag>().HasQueryFilter(p => !p.IsRemove);

            modelBuilder.Entity<Menu>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<ChildMenu>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<ChildMenu_Category>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<ChildMenu_Tag>().HasQueryFilter(p => !p.IsRemove);

            modelBuilder.Entity<HomePageView>().HasQueryFilter(p => !p.IsRemove);
            
        }
    }
}
