using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.News.Commands.AddNews;
using ZNews.Application.Services.News.Commands.EditNews;
using ZNews.Application.Services.News.Commands.RemoveNews;
using ZNews.Application.Services.News.Commands.SliderChanges;
using ZNews.Application.Services.News.Commands.StatusChange;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForAdmin;
using ZNews.Application.Services.News.Queries.GetNewsForAdmin;
using ZNews.Application.Services.News.Queries.GetNewsForUserActivity;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface INewsFacadForAdmin
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddNewsService AddNewsService { get; }
        StatusChangeNewsService StatusChangeNewsService { get; }
        SliderChangesNewsServices SliderChangesNewsServices { get; }
        RemoveNewsService RemoveNewsService { get; }
        EditNewsService EditNewsService { get; }
        /// <summary>
        /// Queries
        /// </summary>
        GetNewsForAdminService GetNewsForAdminService { get; }
        GetDetailsNewsForAdminService GetDetailsNewsForAdminService { get; }
        GetNewsForUserActivityService GetNewsForUserActivityService { get; }
    }
}
