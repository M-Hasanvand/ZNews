using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.Services.Comments.Commands.DeleteComment;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Commands.ChangesStatusComment
{
    public interface IChangesStatusCommentService
    {
        ResultDto Execute(long CommentId);
    }
    public class ChangesStatusCommentService : IChangesStatusCommentService
    {
        private readonly IDataBaseContext _context;
        public ChangesStatusCommentService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long CommentId)
        {
            var comment = _context.Comments.Find(CommentId);
            if (comment == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نظر مورد نظر موجو نیست"
                };
            }
            comment.IsActive = comment.IsActive == true ? false : true;
            string status = comment.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"نظر مورد نظر{status} شد",
            };
        }
    }
}
