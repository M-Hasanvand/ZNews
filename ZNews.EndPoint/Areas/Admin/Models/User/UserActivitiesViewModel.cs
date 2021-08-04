using System.Collections.Generic;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForUserActivity;
using ZNews.Application.Services.Comments.Queries.GetCommentsForUserActivity;
using ZNews.Application.Services.Menus.Queries.GetChildMenusForUserActivity;
using ZNews.Application.Services.Menus.Queries.GetMenusForUserActivity;
using ZNews.Application.Services.News.Queries.GetNewsForUserActivity;
using ZNews.Application.Services.Tags.Queries.GetTagsForUserActivity;
using ZNews.Application.Services.Users.Queries.GetUsersForUserActivity;

namespace ZNews.EndPoint.Areas.Admin.Models.User
{
    public class UserActivitiesViewModel
    {
       public  List<ResultGetCategoriesForUserActivityDto> ListCategories { get; set; }
       public List<ResultGetListCommentForUserActivityDto> ListComments { get; set; }
       public List<ResultGetUsersForUserActivityDto> ListUsers { get; set; }
       public List<ResultGetNewsForUserActivityDto> ListNews { get; set; }
       public List<ResultGetTagsForUserActivityDto> ListTags { get; set; }
       public List<ResultGetMenusForUserActivityDto> ListMenus { get; set; }
       public List<ResultGetChildMenusForUserActivityDto> ListChildMenus { get; set; }
    }
}
