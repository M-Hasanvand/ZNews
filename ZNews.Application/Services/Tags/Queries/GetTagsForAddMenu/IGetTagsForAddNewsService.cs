using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Queries.GetTagsForAddMenu
{
    public interface IGetTagsForAddMenuService
    {
        ResultDto<List<ResultGetTagsAddMenuDto>> Execute();
    }
    public class GetTagsForAddMenuService : IGetTagsForAddMenuService
    {
        private readonly IDataBaseContext _context;
        public GetTagsForAddMenuService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetTagsAddMenuDto>> Execute()
        {
            var tags = _context.Tags.Where(p=>p.IsActive==true).Select(p=>new ResultGetTagsAddMenuDto()
            {
                Id=p.Id,
                Name=p.Name,
            }).OrderByDescending(p => p.Id).ToList();
            if(tags.Count==0)
            {
                return new ResultDto<List<ResultGetTagsAddMenuDto>>()
                {
                    IsSuccess = false
                };
            }
            return new ResultDto<List<ResultGetTagsAddMenuDto>>()
            {
                Data=tags,
                IsSuccess=true
            };

        }
    }
    public class ResultGetTagsAddMenuDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
