using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.Services.Comments.Queries.GetListCommentForSite;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForSite;

namespace ZNews.EndPoint.Models
{
    public class NewsDetailForSiteViewModel
    {
        public ResultDetailsNewsForSiteDto NewsDetails { get; set; }
        public List<ResultGetListCommentForSiteDto> ListComment { get; set; }
    }
}
