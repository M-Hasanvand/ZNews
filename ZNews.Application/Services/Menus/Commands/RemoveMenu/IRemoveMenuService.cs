using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Commands.RemoveMenu
{
    public interface IRemoveMenuService
    {
        ResultDto Execute(long MenuId);
    }
    public class RemoveMenuService : IRemoveMenuService
    {
        private readonly IDataBaseContext _context;
        public RemoveMenuService(IDataBaseContext context)
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
            var childMenus = _context.ChildMenus.Where(p=>p.ParentId==menu.Id).ToList();
            foreach (var itemchildMenu in childMenus)
            {
                itemchildMenu.RemoveTime = DateTime.Now;
                itemchildMenu.IsRemove = true;
                var categories = _context.ChildMenu_Categories.Where(p => p.ChildMenuId == itemchildMenu.Id);
                foreach (var itemCategory in categories)
                {
                    itemCategory.RemoveTime = DateTime.Now;
                    itemCategory.IsRemove = true;
                }
                var Tags = _context.ChildMenu_Tags.Where(p => p.ChildMenuId == itemchildMenu.Id);
                foreach (var itemTag in Tags)
                {
                    itemTag.RemoveTime = DateTime.Now;
                    itemTag.IsRemove = true;
                }
            }
            menu.RemoveTime = DateTime.Now;
            menu.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "منو مورد نظر حذف شد",
            };
        }
    }
}
