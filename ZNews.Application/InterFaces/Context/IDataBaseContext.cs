using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Domain.Entities.Newses;
using ZNews.Domain.Entities.Users;
using ZNews.Domain.Entities.Menues;
using ZNews.Domain.Entities.HomePage.Views;

namespace ZNews.Application.InterFaces.Context
{
    public interface IDataBaseContext
    {
        //----Entities--------------------------
        DbSet<User> Users { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<NewsInTag> NewsInTags { get; set; }
        DbSet<News> News { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Menu> Menus { get; set; }
        DbSet<ChildMenu> ChildMenus { get; set; }
        DbSet<ChildMenu_Category> ChildMenu_Categories { get; set; }
        DbSet<ChildMenu_Tag> ChildMenu_Tags { get; set; }
        DbSet<HomePageView> HomePageViews { get; set; }

        ///--------------------SaveChanges-------------------------------------
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
