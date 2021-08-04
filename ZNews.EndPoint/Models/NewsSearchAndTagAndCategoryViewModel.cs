using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.Services.News.Queries.GetNewsSearchAndTagAndCategoryForSite;

namespace ZNews.EndPoint.Models
{
    public class NewsSearchAndTagAndCategoryViewModel
    {
        public PagingResultGetNewsSearchAndTagAndCategoryForSiteDto Data { get; set; }
    }
}
