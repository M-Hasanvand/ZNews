
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Queries.GetTagsForSite
{
    public interface IGetTagsForSiteService
    {
        ResultDto<List<ResultGetTagsForSiteDto>> Execute();
    }
    public class GetTagsForSiteService : IGetTagsForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetTagsForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetTagsForSiteDto>> Execute()
        {
            var tags = _context.Tags.Where(p=>p.IsActive==true).Select(p=>new ResultGetTagsForSiteDto()
            {
                Id=p.Id,
                Name=p.Name,
            }).OrderByDescending(p => p.Id).ToList();
            if(tags.Count==0)
            {
                return new ResultDto<List<ResultGetTagsForSiteDto>>()
                {
                    IsSuccess = false
                };
            }
            //6037997179453098
            return new ResultDto<List<ResultGetTagsForSiteDto>>()
            {
                Data=tags,
                IsSuccess=true
            };

        }
    }
    public class ResultGetTagsForSiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
