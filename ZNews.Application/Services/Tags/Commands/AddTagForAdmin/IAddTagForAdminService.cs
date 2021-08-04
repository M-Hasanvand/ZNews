using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.Tags.Commands.AddTagForAdmin
{
    public interface IAddTagForAdminService
    {
        ResultDto Execute(RequestAddTagDto request);
    }
    public class AddTagService: IAddTagForAdminService
    {
        private readonly IDataBaseContext _context;
        public AddTagService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestAddTagDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام تگ را وارد کنید"
                };
            }
            Tag tag = new Tag()
            {
                Name = request.Name,
                UserId = request.UserId,
                IsActive=true
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess=true,
                Message= "تگ با موفقیت اضافه شد"
            };
        }
    }
    public class RequestAddTagDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
    }
}
