using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Categories.Queries.GetCategoriesForUserActivity
{
    public interface IGetCategoriesForUserActivityService
    {
        ResultDto<List<ResultGetCategoriesForUserActivityDto>> Execute(long UserId);
    }
    public class GetCategoriesForUserActivityService : IGetCategoriesForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetCategoriesForUserActivityDto>> Execute(long UserId)
        {
            if(UserId==0)
            {
                return new ResultDto<List<ResultGetCategoriesForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message="ایدی ادمین ارسال نشد"
                };
            }
            var categories = _context.Categories.Where(p=>p.UserId==UserId).Select(p => new ResultGetCategoriesForUserActivityDto()
            {
                Id = p.Id,
                IsActive = p.IsActive,
                Name = p.Name,
                InsertTime=p.InsertTime
            }).OrderByDescending(p => p.InsertTime).ToList();
            if(categories.Count==0)
            {
                return new ResultDto<List<ResultGetCategoriesForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " هیچ دسته بندی اضافه نشده"
                };
            }
            return new ResultDto<List<ResultGetCategoriesForUserActivityDto>>()
            {
                Data=categories,
                IsSuccess=true
            };
        }
    }

    public class ResultGetCategoriesForUserActivityDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
