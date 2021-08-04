using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.News.Commands.SliderChanges
{
    public interface ISliderChangesNewsServices
    {
        ResultDto Execute(long NewsId);
    }
    public class SliderChangesNewsServices: ISliderChangesNewsServices
    {
        private readonly IDataBaseContext _context;
        public SliderChangesNewsServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long NewsId)
        {
            var news = _context.News.Find(NewsId);
            if (news == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خبر مورد نظر یافت نشد"
                };
            }
            news.IsSlider = news.IsSlider == true ? false : true;
            string status = news.IsSlider == false ? "از اسلایدر پاک" : "به اسلایدر اضافه ";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"   خبر مورد   {status}   شد",
            };
        }
    }
}
