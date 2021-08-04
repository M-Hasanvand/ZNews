using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Categories.Queries.GetCategoriesForAddMenu
{
    public interface IGetCategoriesForAddMenuService
    {
        ResultDto<List<ResultCategoriesForAddMenuDto>> Execute();
    }
    public class GetCategoriesForAddMenuService : IGetCategoriesForAddMenuService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesForAddMenuService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultCategoriesForAddMenuDto>> Execute()
        {
            var categories = _context.Categories.Where(p=>p.IsActive==true).Select(p => new ResultCategoriesForAddMenuDto()
            {
                Id = p.Id,
                Name = p.Name
            }).OrderByDescending(p => p.Id).ToList();
            if (categories.Count == 0)
            {
                return new ResultDto<List<ResultCategoriesForAddMenuDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                };
            }
            return new ResultDto<List<ResultCategoriesForAddMenuDto>>()
            {
                Data = categories,
                IsSuccess = true,
            };
        }
    }
    public class ResultCategoriesForAddMenuDto
    {
        public long Id { get; set; }

        
        public string Name { get; set; }
    }
}
