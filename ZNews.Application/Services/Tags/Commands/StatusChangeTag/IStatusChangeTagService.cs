using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Commands.StatusChangeTag
{
    public interface IStatusChangeTagService
    {
        ResultDto Execute(long TagId);
    }
    public class StatusChangeTagService : IStatusChangeTagService
    {
        private readonly IDataBaseContext _context;
        public StatusChangeTagService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long TagId)
        {
            var tag = _context.Tags.Find(TagId);
            if (tag == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تگ مورد نظر یافت نشد"
                };
            }
            tag.IsActive = tag.IsActive == true ? false : true;
            string status = tag.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"تگ مورد نظر{status} شد",
            };
        }
    }
}
