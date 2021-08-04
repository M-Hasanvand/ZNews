
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Queries.GetTagsForAdmin
{
    public interface IGetTagsForSiteService
    {
        ResultDto<List<ResultGetTagsDto>> Execute();
    }
    public class GetTagsForAdminService : IGetTagsForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetTagsForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetTagsDto>> Execute()
        {
            var tags = _context.Tags.Select(p=>new ResultGetTagsDto()
            {
                Id=p.Id,
                Name=p.Name,
                IsActive=p.IsActive
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
        public bool IsActive { get; set; }
    }
}
