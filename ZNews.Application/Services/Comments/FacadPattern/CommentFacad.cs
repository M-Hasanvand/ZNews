using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForUserActivity;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForAdmin;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForSite;
using ZNews.Application.Services.Comments.Commands.ChangesStatusComment;
using ZNews.Application.Services.Comments.Commands.DeleteComment;
using ZNews.Application.Services.Comments.Commands.EditeComment;
using ZNews.Application.Services.Comments.Queries.GetCommentsForUserActivity;
using ZNews.Application.Services.Comments.Queries.GetListCommentForAdmin;
using ZNews.Application.Services.Comments.Queries.GetListCommentForSite;

namespace ZNews.Application.Services.Comments.FacadPattern
{
    public class CommentFacad:ICommentFacad
    {
        private readonly IDataBaseContext _context;
        public CommentFacad(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private AddNewCommentForSiteService _addNewCommentForSiteService;
        public AddNewCommentForSiteService AddNewCommentForSiteService => _addNewCommentForSiteService?? new AddNewCommentForSiteService(_context);

        private AddNewCommentForAdminService _addNewCommentForAdminService;
        public AddNewCommentForAdminService AddNewCommentForAdminService => _addNewCommentForAdminService??new AddNewCommentForAdminService(_context);

        private EditeCommentService _editeCommentService ;
        public EditeCommentService EditeCommentService => _editeCommentService?? new EditeCommentService(_context);

        private DeleteCommentService _deleteCommentService;
        public DeleteCommentService DeleteCommentService =>_deleteCommentService??new DeleteCommentService(_context);

        private ChangesStatusCommentService _changesStatusCommentService;
        public ChangesStatusCommentService ChangesStatusCommentService => _changesStatusCommentService??new ChangesStatusCommentService(_context);

        /// <summary>
        /// Queries
        /// </summary>
        private GetListCommentForAdminService _getListCommentForAdminService;
        public GetListCommentForAdminService GetListCommentForAdminService => _getListCommentForAdminService??new GetListCommentForAdminService(_context);

        private GetCommentsForUserActivityService _getCommentsForUserActivityService;
        public GetCommentsForUserActivityService GetCommentsForUserActivityService => _getCommentsForUserActivityService ?? new GetCommentsForUserActivityService(_context);

        private GetListCommentForSiteService _getListCommentForSiteService;
        public GetListCommentForSiteService GetListCommentForSiteService => _getListCommentForSiteService?? new GetListCommentForSiteService(_context);

    }
}
