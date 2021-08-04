using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Tags.Commands.RemoveTag
{
    public interface IRemoveTagService
    {
        ResultDto Execute(long TagId);

    }
    public class RemoveTagService : IRemoveTagService
    {
        private readonly IDataBaseContext _context;
        public RemoveTagService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long TagId)
        {
            var tag = _context.Tags.Find(TagId);
            if (tag == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تگ مورد نظر یافت نشد"
                };
            }
            var newsInTags = _context.NewsInTags.Where(p => p.TagId == tag.Id).ToList();
            foreach (var itemNewsInTag in newsInTags)
            {
                itemNewsInTag.RemoveTime = DateTime.Now;
                itemNewsInTag.IsRemove = true;
            }
            var childMenuInTags = _context.ChildMenu_Tags.Where(p => p.TagId == tag.Id).ToList();
            foreach (var itemChildMenuInTag in childMenuInTags)
            {
                itemChildMenuInTag.RemoveTime = DateTime.Now;
                itemChildMenuInTag.IsRemove = true;
            }
            tag.RemoveTime = DateTime.Now;
            tag.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "تگ مورد نظر حذف شد",
            };
        }
    }
}
