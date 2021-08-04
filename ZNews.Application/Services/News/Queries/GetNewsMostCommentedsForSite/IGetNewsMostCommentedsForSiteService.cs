using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.News.Queries.GetNewsMostCommentedsForSite
{
    public interface IGetNewsMostCommentedsForSiteService
    {
        ResultDto<List<ResultGetNewsMostCommentedForSiteDto>> Execute();
    }
    public class GetNewsMostCommentedsForSiteService : IGetNewsMostCommentedsForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetNewsMostCommentedsForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetNewsMostCommentedForSiteDto>> Execute()
        {
            var news= _context.News.Include(p=>p.NewsInTages).Where(p=>p.IsActive==true&&p.IsSlider==false&&p.Comments.Count>=10).Select(p => new ResultGetNewsMostCommentedForSiteDto()
            {
                Id = p.Id,
                Title = p.Title,
                UserName=p.User.FullName,
                InsertTime = p.InsertTime,
                CommentsNumber = p.Comments.Count(),
                CoverNews =p.ImageUrl,
            }).OrderByDescending(p => p.CommentsNumber).Take(8).ToList();
            return new ResultDto<List<ResultGetNewsMostCommentedForSiteDto>>()
            {
                Data = news,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetNewsMostCommentedForSiteDto
    {
        public long Id { get; set; }
        public long CommentsNumber { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string CoverNews { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
