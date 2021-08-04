using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.HomePageSite.Queries.GetViewCountForAdmin
{
    public interface IGetViewCountForAdmin
    {
        ResultDto<long> ToDay();
        ResultDto<long> All();
    }
    public class GetViewCountForAdmin : IGetViewCountForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetViewCountForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<long> ToDay()
        {
            var homepageview = _context.HomePageViews.Where(p =>  p.DateView.Day == DateTime.Now.Day).Count();
            return new ResultDto<long>()
            {
                Data = homepageview,
                IsSuccess = true
            };
        }

        public ResultDto<long> All()
        {
            var homepageview = _context.HomePageViews.Count();
            return new ResultDto<long>()
            {
                Data = homepageview,
                IsSuccess = true
            };
        }
    }
}
