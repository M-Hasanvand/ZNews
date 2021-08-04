
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForAdmin;
using ZNews.Application.Services.Comments.Commands.EditeComment;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly ICommentFacad _commentFacad;
        public CommentsController(ICommentFacad commentFacad)
        {
            _commentFacad = commentFacad;
        }
        #region [List]
        [HttpPost]
        public IActionResult List(long NewsId)
        {
            return Json(_commentFacad.GetListCommentForAdminService.Execute(NewsId).Data);
        }
        #endregion
        #region [Add]
        [HttpPost]
        public IActionResult Add(RequestAddNewCommentForAdminDto request)
        {
            return Json(_commentFacad.AddNewCommentForAdminService.Execute(new RequestAddNewCommentForAdminDto()
            {
                NewsId=request.NewsId,
                Text=request.Text,
                ParentId=request.ParentId,
                UserId=request.UserId,
            }));
        }
        #endregion
        #region [Edit]
        [HttpPost]
        public IActionResult Edit(RequestEditeCommentDto request)
        {
            return Json(_commentFacad.EditeCommentService.Execute(new RequestEditeCommentDto()
            {
                CommentId=request.CommentId,
                Text=request.Text
            }));
        }
        #endregion
        #region [Delete]
        [HttpPost]
        public IActionResult Delete(long CommentId)
        {
            return Json(_commentFacad.DeleteCommentService.Execute(CommentId));
        }
        #endregion
        #region [ChangeStatus]
        [HttpPost]
        public IActionResult ChangeStatus(long CommentId)
        {
            return Json(_commentFacad.ChangesStatusCommentService.Execute(CommentId));
        }
        #endregion
    }
}
