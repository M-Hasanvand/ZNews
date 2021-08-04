using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Queries.GetTagsForAddNews
{
    public interface IGetTagsForAddNewsService
    {
        ResultDto<List<ResultGetTagsDto>> Execute();
    }
    public class GetTagsForAddNewsService : IGetTagsForAddNewsService
    {
        private readonly IDataBaseContext _context;
        public GetTagsForAddNewsService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetTagsDto>> Execute()
        {
            var tags = _context.Tags.Where(p=>p.IsActive==true).Select(p=>new ResultGetTagsDto()
            {
                Id=p.Id,
                Name=p.Name,
            }).OrderByDescending(p => p.Id).ToList();
            if(tags.Count==0)
            {
                return new ResultDto<List<ResultGetTagsDto>>()
                {
                    IsSuccess = false
                };
            }
            return new ResultDto<List<ResultGetTagsDto>>()
            {
                Data=tags,
                IsSuccess=true
            };

        }
    }
    public class ResultGetTagsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
