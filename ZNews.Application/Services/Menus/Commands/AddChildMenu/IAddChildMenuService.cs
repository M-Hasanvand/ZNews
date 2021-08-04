using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Menues;

namespace ZNews.Application.Services.Menus.Commands.AddMenu
{
    public interface IAddChildMenuService
    {
        ResultDto Execute(RequestAddChildMenuDto request);
    }
    public class AddChildMenuService : IAddChildMenuService
    {
        private readonly IDataBaseContext _context;
        public AddChildMenuService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestAddChildMenuDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام زیر منو را وارد کنید"
                };
            }
            var menu = _context.Menus.Find(request.ParentId);
            ChildMenu childMenu = new ChildMenu()
            {
                Name = request.Name,
                ParentId=menu.Id,
                Menu=menu,
                UserId = request.UserId,
                IsActive = true
            };
            _context.ChildMenus.Add(childMenu);
            //----------------------------listTags------------------------------------------------------
            List<ChildMenu_Tag> childMenu_Tags = new List<ChildMenu_Tag>();
            if (request.TagsMenuDto != null)
            {
                foreach (var itemTag in request.TagsMenuDto)
                {
                    var tag = _context.Tags.Find(itemTag.TagId);
                    childMenu_Tags.Add(new ChildMenu_Tag()
                    {
                        TagId = tag.Id,
                        Tag = tag,
                        ChildMenu = childMenu,
                        ChildMenuId = childMenu.Id
                    });
                }
            }
            //------------------------------------listCategories------------------------------------------
            List<ChildMenu_Category> childMenu_Categories = new List<ChildMenu_Category>();
            if (request.CategoriesMenuDto!=null)
            {
                foreach (var itemCate in request.CategoriesMenuDto)
                {
                    var category = _context.Categories.Find(itemCate.CateId);
                    childMenu_Categories.Add(new ChildMenu_Category()
                    {
                        CategoryId = category.Id,
                        Category = category,
                        ChildMenu = childMenu,
                        ChildMenuId = childMenu.Id
                    });
                }
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "حداقل یک دسته بندی باید انتخاب شود"
                };
            }
            _context.ChildMenu_Categories.AddRange(childMenu_Categories);
            _context.ChildMenu_Tags.AddRange(childMenu_Tags);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = " زیر منو با موفقیت اضافه شد"
            };
        }
    }
    public class RequestAddChildMenuDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        public long ParentId { get; set; }
        public List<RequestCategoryMenuDto> CategoriesMenuDto { get; set; }
        public List<RequestTagMenuDto> TagsMenuDto { get; set; }
    }
    public class RequestCategoryMenuDto
    {
        public long CateId { get; set; }

    }
    public class RequestTagMenuDto
    {
        public long TagId { get; set; }
    }
}
