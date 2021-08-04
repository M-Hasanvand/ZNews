using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.Services.Comments.Queries.GetListCommentForAdmin;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForAdmin;

namespace ZNews.EndPoint.Areas.Admin.Models.News
{
    public class NewsDetailForAdminViewModel
    {
        public ResultDetailsNewsForAdminDto NewsDetail{ get; set; }
        public List<ResultGetListCommentForAdminDto> ListComments { get; set; } = new List<ResultGetListCommentForAdminDto>();
    }
}
