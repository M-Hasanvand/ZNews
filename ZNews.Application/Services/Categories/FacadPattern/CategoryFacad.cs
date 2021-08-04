using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Categories.Commands.AddCategoryForAdmin;
using ZNews.Application.Services.Categories.Commands.RemoveCategory;
using ZNews.Application.Services.Categories.Commands.StatusChangeCategory;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddMenu;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAddNews;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForAdmin;
using ZNews.Application.Services.Categories.Queries.GetCategoriesForUserActivity;

namespace ZNews.Application.Services.Categories.FacadPattern
{
    public class CategoryFacad:ICategoryFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public CategoryFacad(IDataBaseContext context,IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private AddCategoryService _addCategoryService;
        public AddCategoryService AddCategoryService=> _addCategoryService??new AddCategoryService(_context);

        private RemoveCategoryService _removeCategoryService;
        public RemoveCategoryService RemoveCategoryService => _removeCategoryService ?? new RemoveCategoryService(_context, _environment);

        private StatusChangeCategoryService _statusChangeCategoryService;
        public StatusChangeCategoryService StatusChangeCategoryService => _statusChangeCategoryService ?? new StatusChangeCategoryService(_context);
        /// <summary>
        /// Queries
        /// </summary>
        private GetCategoriesForAdminService _getCategoriesForAdminService;
        public GetCategoriesForAdminService GetCategoriesForAdminService => _getCategoriesForAdminService ?? new GetCategoriesForAdminService(_context);


        private GetCategoriesForAddMenuService _getCategoriesForAddMenuService;
        public GetCategoriesForAddMenuService GetCategoriesForAddMenuService => _getCategoriesForAddMenuService ?? new GetCategoriesForAddMenuService(_context);


        private GetCategoriesForAddNewsService _getCategoriesForAddNewsService;
        public GetCategoriesForAddNewsService GetCategoriesForAddNewsService => _getCategoriesForAddNewsService ?? new GetCategoriesForAddNewsService(_context);
        
        private GetCategoriesForUserActivityService _getCategoriesForUserActivityService;
        public GetCategoriesForUserActivityService GetCategoriesForUserActivityService =>_getCategoriesForUserActivityService ?? new GetCategoriesForUserActivityService(_context);
    }
}
