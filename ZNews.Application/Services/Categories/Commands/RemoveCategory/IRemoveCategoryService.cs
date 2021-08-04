using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;

namespace ZNews.Application.Services.Categories.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        ResultDto Execute(long CategoryId);
    }
    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public RemoveCategoryService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
            var news = _context.News.Where(p => p.CategoryId == category.Id).ToList();
            foreach (var itemNews in news)
            {
                itemNews.RemoveTime = DateTime.Now;
                itemNews.IsRemove = true;
                if (!string.IsNullOrWhiteSpace(itemNews.ImageUrl))
                {
                    itemNews.ImageUrl = Extension.MoveFile(itemNews.ImageUrl, @"RecycleBin\News\Cover\", _environment);
                }
            }
            var newsInCategories = _context.ChildMenu_Categories.Where(p => p.CategoryId == category.Id).ToList();
            foreach (var itemNewsInCategories in newsInCategories)
            {
                itemNewsInCategories.RemoveTime = DateTime.Now;
                itemNewsInCategories.IsRemove = true;
            }
            category.RemoveTime = DateTime.Now;
            category.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته مورد نظر و خبر های مرتبط حذف شدند",
            };
        }
    }
}
