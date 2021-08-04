using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Categories.Queries.GetCategoriesForAdmin
{
    public interface IGetCategoriesForAdminService
    {
        ResultDto<List<ResultGetCategoriesForAdminDto>> Execute();
    }
    public class GetCategoriesForAdminService : IGetCategoriesForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetCategoriesForAdminDto>> Execute()
        {
            var categories = _context.Categories.Select(p=>new ResultGetCategoriesForAdminDto()
            {
                Id=p.Id,
                IsActive=p.IsActive,
                Name=p.Name
            }).OrderByDescending(p => p.Id).ToList();
            if(categories.Count==0)
            {
                return new ResultDto<List<ResultGetCategoriesForAdminDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                };
            }
            return new ResultDto<List<ResultGetCategoriesForAdminDto>>()
            {
                Data = categories,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetCategoriesForAdminDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
