using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForAdmin;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForSite;
using ZNews.Application.Services.Comments.Commands.ChangesStatusComment;
using ZNews.Application.Services.Comments.Commands.DeleteComment;
using ZNews.Application.Services.Comments.Commands.EditeComment;
using ZNews.Application.Services.Comments.Queries.GetCommentsForUserActivity;
using ZNews.Application.Services.Comments.Queries.GetListCommentForAdmin;
using ZNews.Application.Services.Comments.Queries.GetListCommentForSite;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface ICommentFacad
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddNewCommentForSiteService AddNewCommentForSiteService { get; }
        AddNewCommentForAdminService AddNewCommentForAdminService { get; }
        EditeCommentService EditeCommentService { get; }
        DeleteCommentService DeleteCommentService { get; }
        ChangesStatusCommentService ChangesStatusCommentService { get; }
        /// <summary>
        /// Queries
        /// </summary>
        GetListCommentForAdminService GetListCommentForAdminService { get; }
        GetCommentsForUserActivityService GetCommentsForUserActivityService { get; }
        GetListCommentForSiteService GetListCommentForSiteService { get; }
    }
}
