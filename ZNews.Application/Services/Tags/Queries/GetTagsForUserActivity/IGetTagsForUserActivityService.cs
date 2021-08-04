using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Queries.GetTagsForUserActivity
{
    interface IGetTagsForUserActivityService
    {
        ResultDto<List<ResultGetTagsForUserActivityDto>> Execute(long UserId);
    }
    public class GetTagsForUserActivityService: IGetTagsForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetTagsForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetTagsForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetTagsForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var tags = _context.Tags.Where(p=>p.UserId==UserId).Select(p => new ResultGetTagsForUserActivityDto()
            {
                Id = p.Id,
                Name = p.Name,
                IsActive = p.IsActive,
                InsetTime=p.InsertTime
            }).OrderByDescending(p => p.InsetTime).ToList();
            if (tags.Count == 0)
            {
                return new ResultDto<List<ResultGetTagsForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " هیچ تگی اضافه نشده"
                };
            }
            return new ResultDto<List<ResultGetTagsForUserActivityDto>>()
            {
                Data=tags,
                IsSuccess=true
            };
        }
    }
    #region [TagsDto]
    public class ResultGetTagsForUserActivityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsetTime { get; set; }
    }
    #endregion
}
