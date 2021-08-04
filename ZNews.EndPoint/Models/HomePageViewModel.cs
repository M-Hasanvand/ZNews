using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.Services.News.Queries.GetDailyNewsListForSite;
using ZNews.Application.Services.News.Queries.GetNewsForSliderSite;
using ZNews.Application.Services.News.Queries.GetNewsMostCommentedsForSite;
using ZNews.Application.Services.Tags.Queries.GetTagsForSite;

namespace ZNews.EndPoint.Models
{
    public class HomePageViewModel
    {
        public List<ResultGetNewsForSliderSiteDto> ListSliders { get; set; }
        public List<ResultGetNewsForSiteDto> ListNewsDaily { get; set; }
        public List<ResultGetNewsMostCommentedForSiteDto> ListNewsMostCommented { get; set; }
        public List<ResultGetTagsForSiteDto> ListTags { get; set; }
    }
}
