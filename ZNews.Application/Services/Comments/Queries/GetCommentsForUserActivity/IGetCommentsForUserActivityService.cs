using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Queries.GetCommentsForUserActivity
{
    public interface IGetCommentsForUserActivityService
    {
        ResultDto<List<ResultGetListCommentForUserActivityDto>> Execute(long UserId);
    }
    public class GetCommentsForUserActivityService : IGetCommentsForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetCommentsForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetListCommentForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetListCommentForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var comments = _context.Comments.Where(p => p.UserId==UserId).ToList().Select(p => new ResultGetListCommentForUserActivityDto()
            {
                Id = p.Id,
                Text = p.Text,
                UserId = (long)p.UserId,
                IsActive = p.IsActive,
                InsertTime=p.InsertTime,
                NewsId=p.NewsId
            }).OrderByDescending(p => p.InsertTime).ToList();
            if (comments.Count == 0)
            {
                return new ResultDto<List<ResultGetListCommentForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " کامنتی ثبت نشده"
                };
            }
            return new ResultDto<List<ResultGetListCommentForUserActivityDto>>()
            {
                Data = comments,
                IsSuccess = true
            };
        }
    }
    public class ResultGetListCommentForUserActivityDto
    {
        public long Id { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsActive { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public long NewsId { get; set; }
    }
}
