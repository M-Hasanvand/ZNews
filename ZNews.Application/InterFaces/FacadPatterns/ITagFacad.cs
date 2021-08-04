using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddMenu;
using ZNews.Application.Services.Tags.Commands.AddTagForAdmin;
using ZNews.Application.Services.Tags.Commands.RemoveTag;
using ZNews.Application.Services.Tags.Commands.StatusChangeTag;
using ZNews.Application.Services.Tags.Queries.GetTagsForAddMenu;
using ZNews.Application.Services.Tags.Queries.GetTagsForAddNews;
using ZNews.Application.Services.Tags.Queries.GetTagsForAdmin;
using ZNews.Application.Services.Tags.Queries.GetTagsForSite;
using ZNews.Application.Services.Tags.Queries.GetTagsForUserActivity;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface ITagFacad
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddTagService AddTagService { get; }
        RemoveTagService RemoveTagService { get; }
        StatusChangeTagService StatusChangeTagService { get; }
        /// <summary>
        /// Queries
        /// </summary>
        GetTagsForAdminService GetTagsForAdminService { get;}
        GetTagsForAddNewsService GetTagsForAddNewsService { get;}
        GetTagsForAddMenuService GetTagsForAddMenuService { get; }
        GetTagsForUserActivityService GetTagsForUserActivityService { get; }
        GetTagsForSiteService GetTagsForSiteService { get; }
    }
}
