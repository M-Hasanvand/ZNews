using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Commands.StatusChange
{
    public interface IStatusChangeChildMenuService
    {
        ResultDto Execute(long ChildMenuId);
    }
    public class StatusChangeChildMenuService : IStatusChangeChildMenuService
    {
        private readonly IDataBaseContext _context;
        public StatusChangeChildMenuService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long ChildMenuId)
        {
            var childMenu = _context.ChildMenus.Find(ChildMenuId);
            if (childMenu == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "زیر منو مورد نظر یافت نشد"
                };
            }
            childMenu.IsActive = childMenu.IsActive == true ? false : true;
            string status = childMenu.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $" زیر منو مورد نظر{status} شد",
            };
        }
    }
}
