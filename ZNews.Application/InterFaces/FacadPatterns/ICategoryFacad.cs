using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.Categories.Commands.AddCategoryForAdmin;
using ZNews.Application.Services.Categories.Commands.RemoveCategory;
using ZNews.Application.Services.Categories.Commands.StatusChangeCategory;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddMenu;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddNews;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAdmin;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForUserActivity;
using ZNews.Application.Services.Tags.Commands.StatusChangeTag;

namespace ZNews.Application.InterFaces.FacadPatterns
{

    public interface ICategoryFacad
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddCategoryService AddCategoryService { get; }
        RemoveCategoryService RemoveCategoryService { get; }
        StatusChangeCategoryService StatusChangeCategoryService { get; }
        /// <summary>
        /// Queries
        /// </summary>
        GetCategoriesForAdminService GetCategoriesForAdminService { get; }
        GetCategoriesForAddNewsService GetCategoriesForAddNewsService { get; }
        GetCategoriesForAddMenuService GetCategoriesForAddMenuService { get; }
        GetCategoriesForUserActivityService GetCategoriesForUserActivityService { get; }
    }
}
