using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddMenu;
using ZNews.Application.Services.Tags.Commands.AddTagForAdmin;
using ZNews.Application.Services.Tags.Commands.RemoveTag;
using ZNews.Application.Services.Tags.Commands.StatusChangeTag;
using ZNews.Application.Services.Tags.Queries.GetTagsForAddMenu;
using ZNews.Application.Services.Tags.Queries.GetTagsForAddNews;
using ZNews.Application.Services.Tags.Queries.GetTagsForAdmin;
using ZNews.Application.Services.Tags.Queries.GetTagsForSite;
using ZNews.Application.Services.Tags.Queries.GetTagsForUserActivity;

namespace ZNews.Application.Services.Tags.FacadPattern
{
    public class TagFacad: ITagFacad
    {
        private readonly IDataBaseContext _context;
        public TagFacad(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private AddTagService _addTagService;
        public AddTagService AddTagService=> _addTagService??new AddTagService(_context);

        private RemoveTagService _removeTagService;
        public RemoveTagService RemoveTagService => _removeTagService ?? new RemoveTagService(_context);

        private StatusChangeTagService _statusChangeTagService;
        public StatusChangeTagService StatusChangeTagService => _statusChangeTagService ?? new StatusChangeTagService(_context);
        /// <summary>
        /// Queries
        /// </summary>
        private GetTagsForAdminService _getTagsForAdminService;
        public GetTagsForAdminService GetTagsForAdminService => _getTagsForAdminService ?? new GetTagsForAdminService(_context);

        private GetTagsForAddMenuService _getTagsForAddMenuService;
        public GetTagsForAddMenuService GetTagsForAddMenuService => _getTagsForAddMenuService ?? new GetTagsForAddMenuService(_context);

        private GetTagsForAddNewsService _getTagsForAddNewsService;
        public GetTagsForAddNewsService GetTagsForAddNewsService => _getTagsForAddNewsService ?? new GetTagsForAddNewsService(_context);
        
        private GetTagsForUserActivityService _getTagsForUserActivityService;
        public GetTagsForUserActivityService GetTagsForUserActivityService => _getTagsForUserActivityService ?? new GetTagsForUserActivityService(_context);

        private GetTagsForSiteService _getTagsForSiteService;
        public GetTagsForSiteService GetTagsForSiteService => _getTagsForSiteService?? new GetTagsForSiteService(_context);
    }
}
