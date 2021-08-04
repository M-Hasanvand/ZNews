using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.News.Commands.AddNews;
using ZNews.Application.Services.News.Commands.EditNews;
using ZNews.Application.Services.News.Commands.RemoveNews;
using ZNews.Application.Services.News.Commands.SliderChanges;
using ZNews.Application.Services.News.Commands.StatusChange;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForAdmin;
using ZNews.Application.Services.News.Queries.GetNewsForAdmin;
using ZNews.Application.Services.News.Queries.GetNewsForUserActivity;

namespace ZNews.Application.Services.News.FacadPattern
{
    public class NewsFacadForAdmin : INewsFacadForAdmin
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public NewsFacadForAdmin(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private AddNewsService _addNewsService;
        public AddNewsService AddNewsService=> _addNewsService??new AddNewsService(_context, _environment);

        private StatusChangeNewsService _statusChangeNewsService;
        public  StatusChangeNewsService StatusChangeNewsService => _statusChangeNewsService ?? new StatusChangeNewsService(_context);
        
        private SliderChangesNewsServices _sliderChangesNewsServices;
        public SliderChangesNewsServices SliderChangesNewsServices => _sliderChangesNewsServices ?? new SliderChangesNewsServices(_context);
        private RemoveNewsService _removeNewsSerivce;
        public RemoveNewsService RemoveNewsService => _removeNewsSerivce ?? new RemoveNewsService(_context,_environment);

        public EditNewsService _editNewsService;
        public EditNewsService EditNewsService => _editNewsService??new EditNewsService(_context);
        /// <summary>
        /// Queries
        /// </summary>
        private GetNewsForAdminService _getNewsForAdminService;
        public GetNewsForAdminService GetNewsForAdminService => _getNewsForAdminService??new GetNewsForAdminService(_context);

        private GetDetailsNewsForAdminService _getDetailsNewsForAdminService;
        public GetDetailsNewsForAdminService GetDetailsNewsForAdminService => _getDetailsNewsForAdminService ?? new GetDetailsNewsForAdminService(_context);
        
        private GetNewsForUserActivityService _getNewsForUserActivityService;
        public GetNewsForUserActivityService GetNewsForUserActivityService => _getNewsForUserActivityService ?? new GetNewsForUserActivityService(_context);

    }
}
