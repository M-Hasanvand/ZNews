
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Commands.DeleteComment
{
    public interface IDeleteCommentService
    {
        ResultDto Execute(long CommentId);
    }
    public class DeleteCommentService : IDeleteCommentService
    {
        private readonly IDataBaseContext _context;
        public DeleteCommentService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long CommentId)
        {
            var comment = _context.Comments.Find(CommentId);
            var childComment = _context.Comments.Where(p=>p.ParentId==CommentId).ToList();
            if (comment == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نظر مورد نظر موجو نیست"
                };
            }
            
            comment.IsRemove = true;
            comment.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            foreach (var item in childComment)
            {
                item.IsRemove = true;
                item.RemoveTime = DateTime.Now;
                _context.SaveChanges();
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "نظر حذف شد"
            };
        }
    }
}
