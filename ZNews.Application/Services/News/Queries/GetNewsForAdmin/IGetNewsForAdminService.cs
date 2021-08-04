using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.News.Queries.GetNewsForAdmin
{
    public interface IGetNewsForSliderSiteService
    {
        ResultDto<PagingResultGetNewsForAdminDto> Execute(RequestGetNewsForAdminDto request);
    }
    public class GetNewsForAdminService : IGetNewsForSliderSiteService
    {
        private readonly IDataBaseContext _context;
        public GetNewsForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<PagingResultGetNewsForAdminDto> Execute(RequestGetNewsForAdminDto request)
        {
            var query = _context.News.Include(p=>p.User)
                .Include(p=>p.Category)
                .Include(p=>p.NewsInTages)
                .ThenInclude(p=>p.Tag).AsQueryable();
            if(!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(p => p.Title.Contains(request.SearchKey) || p.Description.Contains(request.SearchKey));
            }
            List<ResultGetNewsForAdminDto> news=new List<ResultGetNewsForAdminDto>();
            switch (request.ordering)
            {
                case Ordering.Newest:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber = commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {
                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderByDescending(p => p.InsertTime).ToList();
                    break;
                case Ordering.Oldest:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber = commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {
                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderBy(p => p.InsertTime).ToList();
                    break;
                case Ordering.MostVisited:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber = commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {
                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderByDescending(p=>p.Views).ToList();
                    break;
                case Ordering.MostControversial:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber= commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {
                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderByDescending(p=>p.CommentsNumber).ToList();
                    break;
                case Ordering.AddedBSlider:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Where(p=>p.IsSlider==true).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber = commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {
                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderByDescending(p => p.InsertTime).ToList();
                    break;
                case Ordering.Disabled:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Where(p=>p.IsActive==false).Select(p =>
                    new ResultGetNewsForAdminDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Views = p.Views,
                        IsActive = p.IsActive,
                        CategoryName = p.Category.Name,
                        InsertTime = p.InsertTime,
                        IsSlider = p.IsSlider,
                        CommentsNumber = commentsNumber(p.Id),
                        Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                        {

                            Name = t.Tag.Name
                        }).ToList(),
                        UserGuid = p.User.UserGuid,
                        UserId = p.User.Id,
                    }).OrderByDescending(p => p.InsertTime).ToList();
                    break;
                default:
                    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
                     new ResultGetNewsForAdminDto()
                     {
                         Id = p.Id,
                         Title = p.Title,
                         Views = p.Views,
                         IsActive = p.IsActive,
                         CategoryName = p.Category.Name,
                         InsertTime = p.InsertTime,
                         IsSlider = p.IsSlider,
                         CommentsNumber = commentsNumber(p.Id),
                         Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).ToList().Select(t => new TagsGetNewsForAdminDto()
                         {
                             Name = t.Tag.Name
                         }).ToList(),
                         UserGuid = p.User.UserGuid,
                         UserId = p.User.Id,
                     }).OrderByDescending(p => p.InsertTime).ToList();
                    break;
            }
            return new ResultDto<PagingResultGetNewsForAdminDto>()
            {
                Data = new PagingResultGetNewsForAdminDto()
                {
                    CurrentPage = request.CurrentPage,
                    NewsCount =query.Count(),
                    PageSize = request.PageSize,
                    News = news
                },
                IsSuccess = true,
            };
        }
        private long commentsNumber(long NewsId)
        {
            return _context.Comments.Where(p => p.NewsId == NewsId).Count();
        }
    }
    public class PagingResultGetNewsForAdminDto
    {
        public int NewsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ResultGetNewsForAdminDto> News { get; set; } = new List<ResultGetNewsForAdminDto>();
    }
    public class ResultGetNewsForAdminDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime InsertTime { get; set; }
        public long Views { get; set; }
        public long CommentsNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        public Guid UserGuid { get; set; }
        public long UserId { get; set; }
        public virtual List<TagsGetNewsForAdminDto> Tags { get; set; }

    }
    public class TagsGetNewsForAdminDto
    {
        public string Name { get; set; }
    }
    public class RequestGetNewsForAdminDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchKey { get; set; }
        public Ordering ordering { get; set; }
    }
    public enum Ordering
    {
        /// <summary>
        /// جدیدترین ها
        /// </summary>
        Newest = 0,
        /// <summary>
        /// قدیمی ترین ها
        /// </summary>
        Oldest = 1,
        /// <summary>
        /// پربازدیدترین ها
        /// </summary>
        MostVisited = 2,
        /// <summary>
        /// پربحث ترین ها
        /// </summary>
        MostControversial = 3,
        /// <summary>
        /// غیرفعال ها
        /// </summary>
        Disabled=4,
        /// <summary>
        /// اضافه شده ها به اسلایدر
        /// </summary>
        AddedBSlider = 5,
    }
}
