using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.News.Commands.StatusChange
{
    public interface IStatusChangeNewsService
    {
        ResultDto Execute(long NewsId);
    }
    public class StatusChangeNewsService : IStatusChangeNewsService
    {
        private readonly IDataBaseContext _context;
        public StatusChangeNewsService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long NewsId)
        {
            var news = _context.News.Find(NewsId);
            if (news == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خبر مورد نظر یافت نشد"
                };
            }
            news.IsActive = news.IsActive == true ? false : true;
            string status = news.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"   خبر مورد نظر  {status}  شد",
            };
        }
    }
}
