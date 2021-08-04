using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Categories.Commands.StatusChangeCategory
{
    public interface IStatusChangeCategoryService
    {
        ResultDto Execute(long CategoryId);
    }
    public class StatusChangeCategoryService : IStatusChangeCategoryService
    {
        private readonly IDataBaseContext _context;
        public StatusChangeCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long CategoryId)
        {
            var category = _context.Categories.Find(CategoryId);
            if (category == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته مورد نظر یافت نشد"
                };
            }
            category.IsActive = category.IsActive == true ? false : true;
            string status = category.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"دسته مورد نظر {status} شد",
            };
        }
    }
}
