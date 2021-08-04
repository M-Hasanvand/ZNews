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
    public interface IAddMenuService
    {
        ResultDto Execute(RequestAddMenuDto request);
    }
    public class AddMenuService : IAddMenuService
    {
        private readonly IDataBaseContext _context;
        public AddMenuService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestAddMenuDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام منو را وارد کنید"
                };
            }
            Menu menu = new Menu()
            {
                Name = request.Name,
                UserId = request.UserId,
                IsActive = true
            };
            _context.Menus.Add(menu);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "منو با موفقیت اضافه شد"
            };
        }
    }
    public class RequestAddMenuDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
    }
}
