using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Categories.Queries.GetCategoriesForAddNews
{
    public interface IGetCategoriesForAddNewsService
    {
        ResultDto<List<ResultGetCategoriesForAddNewsDto>> Execute();
    }
    public class GetCategoriesForAddNewsService : IGetCategoriesForAddNewsService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesForAddNewsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetCategoriesForAddNewsDto>> Execute()
        {
            var categories = _context.Categories.Where(p=>p.IsActive==true).Select(p => new ResultGetCategoriesForAddNewsDto()
            {
                Id = p.Id,
                Name = p.Name
            }).OrderByDescending(p => p.Id).ToList();
            if (categories.Count == 0)
            {
                return new ResultDto<List<ResultGetCategoriesForAddNewsDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                };
            }
            return new ResultDto<List<ResultGetCategoriesForAddNewsDto>>()
            {
                Data = categories,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetCategoriesForAddNewsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
