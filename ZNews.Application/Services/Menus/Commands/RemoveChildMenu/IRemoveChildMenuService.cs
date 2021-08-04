using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Commands.RemoveChildMenu
{
    public interface IRemoveChildMenuService
    {
        ResultDto Execute(long ChildMenuId);
    }
    public class RemoveChildMenuService : IRemoveChildMenuService
    {
        private readonly IDataBaseContext _context;
        public RemoveChildMenuService(IDataBaseContext context)
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
                    Message = "زیر منو مورد نظر یافت نشد "
                };
            }
            var categories = _context.ChildMenu_Categories.Where(p => p.ChildMenuId == childMenu.Id);
            foreach (var itemCategory in categories)
            {
                itemCategory.RemoveTime = DateTime.Now;
                itemCategory.IsRemove = true;
            }
            var Tags = _context.ChildMenu_Tags.Where(p => p.ChildMenuId == childMenu.Id);
            foreach (var itemTag in Tags)
            {
                itemTag.RemoveTime = DateTime.Now;
                itemTag.IsRemove = true;
            }
            childMenu.RemoveTime = DateTime.Now;
            childMenu.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "زیر منو مورد نظر حذف شد",
            };
        }
    }
}
