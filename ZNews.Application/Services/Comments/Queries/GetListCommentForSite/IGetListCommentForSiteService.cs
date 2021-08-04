using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Queries.GetListCommentForSite
{
    public interface IGetListCommentForSiteService
    {
        ResultDto<List<ResultGetListCommentForSiteDto>> Execute(long NewsId);
    }
    public class GetListCommentForSiteService : IGetListCommentForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetListCommentForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetListCommentForSiteDto>> Execute(long NewsId)
        {
            var comments = _context.Comments.Include(p => p.User).Where(p => p.ParentId == null && p.NewsId == NewsId && p.IsActive == true).ToList().Select(p => new ResultGetListCommentForSiteDto()
            {
                Id = p.Id,
                Email = p.Email,
                FullName = p.FullName,
                ImageUrl = p.UserId == null ? "Images/AdminImage/UserComment.jpg" : "Images/AdminImage/AdminComment.jpg",
                Text = p.Text,
                IsActive = p.IsActive,
                InsertTime = p.InsertTime,
                resultGetChildren = listComments(p.Id)
            }).ToList();
            return new ResultDto<List<ResultGetListCommentForSiteDto>>()
            {
                Data = comments,
                IsSuccess = true
            };
        }
        private List<ResultGetChildListCommentForSiteDto> listComments(long NewsId)
        {
            return _context.Comments.Where(c => c.ParentId == NewsId).Select(rc => new ResultGetChildListCommentForSiteDto()
            {
                Id = rc.Id,
                FullName = rc.FullName,
                Email = rc.Email,
                Text = rc.Text,
                UserId = rc.UserId,
                ImageUrl = rc.UserId == null ? "Images/AdminImage/UserComment.jpg" : "Images/AdminImage/AdminComment.jpg",
                InsertTime = rc.InsertTime,
                IsActive = rc.IsActive,
            }).ToList();
        }
    }
    public class ResultGetListCommentForSiteDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime InsertTime { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Text { get; set; }
        public long? UserId { get; set; }
        public long NewsId { get; set; }
        public List<ResultGetChildListCommentForSiteDto> resultGetChildren { get; set; } = new List<ResultGetChildListCommentForSiteDto>();
    }
    public class ResultGetChildListCommentForSiteDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime InsertTime { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Text { get; set; }
        public long? UserId { get; set; }
        public long NewsId { get; set; }
    }
}
