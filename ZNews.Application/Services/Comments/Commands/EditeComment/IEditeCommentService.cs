using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Comments.Commands.EditeComment
{
    public interface IEditeCommentService
    {
        ResultDto Execute(RequestEditeCommentDto request);
    }
    public class EditeCommentService : IEditeCommentService
    {
        private readonly IDataBaseContext _context;
        public EditeCommentService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditeCommentDto request)
        {
            var comment = _context.Comments.Find(request.CommentId);
            if (comment == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نظر مورد نظر موجو نیست"
                };
            }
            comment.Text = request.Text;
            comment.UpdateTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ویرایش نظر انجام شد"
            };
        }
    }
    public class RequestEditeCommentDto
    {
        public long CommentId { get; set; }
        public string Text { get; set; }
    }
}
