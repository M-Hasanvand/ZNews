using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Commands.StatusChangeMenu
{
    public interface IStatusChangeMenuService
    {
        ResultDto Execute(long MenuId);
    }
    public class StatusChangeMenuService : IStatusChangeMenuService
    {
        private readonly IDataBaseContext _context;
        public StatusChangeMenuService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long MenuId) 
        {
            var menu = _context.Menus.Find(MenuId);
            if (menu == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "منو مورد نظر یافت نشد"
                };
            }
            menu.IsActive = menu.IsActive == true ? false : true;
            string status = menu.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"منو مورد نظر{status} شد",
            };
        }
    }
}
