using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.Categories.Commands.AddCategoryForAdmin
{
    public interface IAddCategoryForAdminService
    {
        ResultDto Execute(RequestAddCategoryDto request);
    }
    public class AddCategoryService: IAddCategoryForAdminService
    {
        private readonly IDataBaseContext _context;
        public AddCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestAddCategoryDto request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام دسته را وارد کنید"
                };
            }
            Category category = new Category()
            {
                Name=request.Name,
                UserId=request.UserId,
                IsActive=true
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته با موفقیت اضافه شد"
            };
        }
    }
    public class RequestAddCategoryDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
    }
}
