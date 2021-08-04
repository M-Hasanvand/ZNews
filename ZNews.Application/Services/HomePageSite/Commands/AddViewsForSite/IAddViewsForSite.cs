using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.HomePage.Views;

namespace ZNews.Application.Services.HomePageSite.Commands.AddViewsForSite
{
    public interface IAddViewsForSite
    {
        ResultDto Execute();
    }
    public class AddViewsForSite: IAddViewsForSite
    {
        private readonly IDataBaseContext _context;
        public AddViewsForSite(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute()
        {
            HomePageView homePageView = new HomePageView()
            {
                DateView = DateTime.Now,
                InsertTime = DateTime.Now
            };
            _context.HomePageViews.Add(homePageView);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true
            };
        }
    }
}
