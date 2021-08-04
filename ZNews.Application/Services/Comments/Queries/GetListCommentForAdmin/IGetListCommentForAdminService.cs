using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Queries.GetListCommentForAdmin
{
    public interface IGetListCommentForAdminService
    {
        ResultDto<List<ResultGetListCommentForAdminDto>> Execute(long NewsId);
    }
    public class GetListCommentForAdminService : IGetListCommentForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetListCommentForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetListCommentForAdminDto>> Execute(long NewsId)
        {
            var comments = _context.Comments.Include(p=>p.User).Where(p=>p.ParentId==null&&p.NewsId==NewsId).ToList().Select(p => new ResultGetListCommentForAdminDto()
            {
                Id=p.Id,
                Email = p.Email,
                FullName = p.FullName,
                Text = p.Text,
                ImageUrl = p.UserId == null ? "Images/AdminImage/UserComment.jpg" : "Images/AdminImage/AdminComment.jpg",
                IsActive=p.IsActive,
                InsertTime=p.InsertTime,
                resultGetChildren = listComments(p.Id)
            }).ToList();
            return new ResultDto<List<ResultGetListCommentForAdminDto>>()
            {
                Data = comments,
                IsSuccess = true
            };
        }
        private List<ResultGetChildListCommentForAdminDto> listComments(long NewsId)
        {
            return _context.Comments.Where(c => c.ParentId ==NewsId).Select(rc => new ResultGetChildListCommentForAdminDto()
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
    public class ResultGetListCommentForAdminDto
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
        public List<ResultGetChildListCommentForAdminDto> resultGetChildren { get; set; } = new List<ResultGetChildListCommentForAdminDto>();
    }
    public class ResultGetChildListCommentForAdminDto
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
