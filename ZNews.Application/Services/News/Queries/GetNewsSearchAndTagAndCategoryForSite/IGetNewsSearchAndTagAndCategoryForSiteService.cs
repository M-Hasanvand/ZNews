using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.News.Queries.GetNewsSearchAndTagAndCategoryForSite
{
    public interface IGetNewsForSliderSiteService
    {
        ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto> Execute(RequestGetNewsSearchAndTagAndCategoryForSiteDto request);
    }
    public class GetNewsSearchAndTagAndCategoryForSiteService : IGetNewsForSliderSiteService
    {
        private readonly IDataBaseContext _context;
        public GetNewsSearchAndTagAndCategoryForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto> Execute(RequestGetNewsSearchAndTagAndCategoryForSiteDto request)
        {
            var query = _context.News.AsQueryable();
            List<ResultGetNewsSearchAndTagAndCategoryForSiteDto> news = new List<ResultGetNewsSearchAndTagAndCategoryForSiteDto>();
            if (request.ChildMenuId != 0)
            {
                //Menu------------Categories------------
                var categories = _context.ChildMenu_Categories.Where(p => p.ChildMenuId == request.ChildMenuId).Select(p=> new categorires()
                {
                    Id=p.CategoryId
                }).ToList();
                foreach (var itemCategory in categories)
                {
                    news.AddRange(query.Where(p => p.CategoryId == itemCategory.Id).Select(p => new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        CategoryName = p.Category.Name,
                        ShortDescription = p.ShortDescription,
                        CategoryId = p.CategoryId,
                        CoverNews = p.ImageUrl,
                        UserName = p.User.FullName,
                        InsertTime = p.InsertTime,
                    }).OrderByDescending(p => p.InsertTime).ToList());
                }
                //Menu------------Tags----------------------------
                var childMenu_tags = _context.ChildMenu_Tags.Where(p => p.ChildMenuId == request.ChildMenuId).ToList();
                List<Tags> news_tags = new List<Tags>();
                foreach (var itemchildMenu_tag in childMenu_tags)
                {
                    news_tags.AddRange(_context.NewsInTags.Where(p => p.TagId == itemchildMenu_tag.TagId).Select(p => new Tags
                    {
                        Id = p.NewsId
                    }).ToList());
                }
                foreach (var itemnews_tag in news_tags)
                {
                    news.AddRange(query.Where(p => p.Id == itemnews_tag.Id).Select(p => new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        CategoryName = p.Category.Name,
                        ShortDescription = p.ShortDescription,
                        CategoryId = p.CategoryId,
                        CoverNews = p.ImageUrl,
                        UserName = p.User.FullName,
                        InsertTime = p.InsertTime,
                    }).OrderByDescending(p => p.InsertTime).ToList());
                }
                news= news.Pagenation(request.CurrentPage,request.PageSize).ToList();
                return new ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto>()
                {
                    Data = new PagingResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        CurrentPage = request.CurrentPage,
                        NewsCount = query.Count(),
                        PageSize = request.PageSize,
                        News = news.Distinct(new DistinctNews()).ToList()
                    },
                    IsSuccess = true,
                };
            }
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                //Menu------------Categories------------
                var categories = _context.Categories.Where(p => p.Name.Contains(request.SearchKey)).Select(p=> new categorires()
                {
                    Id=p.Id
                }).ToList();
                foreach (var itemCategory in categories)
                {
                    news.AddRange(query.Where(p => p.CategoryId == itemCategory.Id).Select(p => new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        CategoryName = p.Category.Name,
                        ShortDescription = p.ShortDescription,
                        CategoryId = p.CategoryId,
                        CoverNews = p.ImageUrl,
                        UserName = p.User.FullName,
                        InsertTime = p.InsertTime,
                    }).OrderByDescending(p => p.InsertTime).ToList());
                }
                //Menu------------Tags----------------------------
                var tags = _context.Tags.Where(p => p.Name.Contains(request.SearchKey)).ToList();
                List<Tags> news_tags = new List<Tags>();
                foreach (var itemtags in tags)
                {
                    news_tags.AddRange(_context.NewsInTags.Where(p => p.TagId == itemtags.Id).Select(p => new Tags
                    {
                        Id = p.NewsId
                    }).ToList());
                }
                foreach (var itemnews_tag in news_tags)
                {
                    news.AddRange(query.Where(p => p.Id == itemnews_tag.Id).Select(p => new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        CategoryName = p.Category.Name,
                        ShortDescription = p.ShortDescription,
                        CategoryId = p.CategoryId,
                        CoverNews = p.ImageUrl,
                        UserName = p.User.FullName,
                        InsertTime = p.InsertTime,
                    }).OrderByDescending(p => p.InsertTime).ToList());
                }
                news = news.Pagenation(request.CurrentPage,request.PageSize).ToList();
                return new ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto>()
                {
                    Data = new PagingResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        CurrentPage = request.CurrentPage,
                        NewsCount = query.Count(),
                        PageSize = request.PageSize,
                        News = news.Distinct(new DistinctNews()).ToList()
                    },
                    IsSuccess = true,
                };
            }
            if (request.TagId!=0)
            {
                //Menu------------Tags----------------------------
                var Tags = _context.Tags.Where(p => p.Id == request.TagId).ToList();
                List<Tags> news_tags = new List<Tags>();
                foreach (var itemTags in Tags)
                {
                    news_tags.AddRange(_context.NewsInTags.Where(p => p.TagId == itemTags.Id).Select(p => new Tags
                    {
                        Id = p.NewsId
                    }).ToList());
                }
                foreach (var itemnews_tag in news_tags)
                {
                    news.AddRange(query.Where(p => p.Id == itemnews_tag.Id).Select(p => new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        CategoryName = p.Category.Name,
                        ShortDescription = p.ShortDescription,
                        CategoryId = p.CategoryId,
                        CoverNews = p.ImageUrl,
                        UserName = p.User.FullName,
                        InsertTime = p.InsertTime,
                    }).OrderByDescending(p => p.InsertTime).ToList());
                }
                news = news.Pagenation(request.CurrentPage, request.PageSize).ToList();
                return new ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto>()
                {
                    Data = new PagingResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        CurrentPage = request.CurrentPage,
                        NewsCount = query.Count(),
                        PageSize = request.PageSize,
                        News = news.Distinct(new DistinctNews()).ToList()
                    },
                    IsSuccess = true,
                };
            }

            else
            {
                return new ResultDto<PagingResultGetNewsSearchAndTagAndCategoryForSiteDto>()
                {
                    Data = new PagingResultGetNewsSearchAndTagAndCategoryForSiteDto()
                    {
                        CurrentPage = request.CurrentPage,
                        NewsCount = query.Count(),
                        PageSize = request.PageSize,
                        News = news.Distinct(new DistinctNews()).ToList()
                    },
                    IsSuccess = true,
                };
            }
        }
    }
    public class DistinctNews : IEqualityComparer<ResultGetNewsSearchAndTagAndCategoryForSiteDto>
    {
        public bool Equals(ResultGetNewsSearchAndTagAndCategoryForSiteDto x, ResultGetNewsSearchAndTagAndCategoryForSiteDto y)
        {
            if(x.Id==y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] ResultGetNewsSearchAndTagAndCategoryForSiteDto obj)
        {
            return obj.GetHashCode();
        }
    }
    public class PagingResultGetNewsSearchAndTagAndCategoryForSiteDto
    {
        public int NewsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ResultGetNewsSearchAndTagAndCategoryForSiteDto> News { get; set; } = new List<ResultGetNewsSearchAndTagAndCategoryForSiteDto>();
    }
    public class ResultGetNewsSearchAndTagAndCategoryForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public string CoverNews { get; set; }
        public string UserName { get; set; }
        public string ShortDescription { get; set; }
        public DateTime InsertTime { get; set; }

    }
    public class RequestGetNewsSearchAndTagAndCategoryForSiteDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public string SearchKey { get; set; }
        public long TagId { get; set; }
        public long CateId { get; set; }
        public long ChildMenuId { get; set; }
    }
    public class categorires
    {
        public long Id { get; set; }
    }
    public class Tags
    {
        public long Id { get; set; }
    }
    #region code
    //var query = _context.News.Include(p => p.User)
    //    .Include(p => p.Category)
    //    .Include(p => p.NewsInTages)
    //    .ThenInclude(p => p.Tag).AsQueryable();
    //List<ResultGetNewsSearchAndTagAndCategoryForSiteDto> news = new List<ResultGetNewsSearchAndTagAndCategoryForSiteDto>();
    //if (!string.IsNullOrWhiteSpace(request.SearchKey))
    //{
    //    query = query.Where(p => p.Title.Contains(request.SearchKey) || p.Description.Contains(request.SearchKey) || p.ShortDescription.Contains(request.SearchKey));
    //    news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
    // new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
    // {
    //     Id = p.Id,
    //     Title = p.Title,
    //     CategoryName = p.Category.Name,
    //     ShortDescription = p.ShortDescription,
    //     CategoryId = p.CategoryId,
    //     CoverNews = p.ImageUrl,
    //     UserName = p.User.FullName,
    //     InsertTime = p.InsertTime,
    // }).OrderByDescending(p => p.InsertTime).ToList();
    //    //categories_Search----------------------------------
    //    var categories = _context.Categories.Where(p => p.Name.Contains(request.SearchKey)).ToList();
    //    foreach (var item in categories)
    //    {
    //        query = query.Where(p => p.CategoryId == item.Id);
    //        news = query.Pagenation(request.CurrentPage, request.PageSize).Select(p =>
    //new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
    //{
    //    Id = p.Id,
    //    Title = p.Title,
    //    CategoryName = p.Category.Name,
    //    ShortDescription = p.ShortDescription,
    //    CategoryId = p.CategoryId,
    //    CoverNews = p.ImageUrl,
    //    UserName = p.User.FullName,
    //    InsertTime = p.InsertTime,
    //}).OrderByDescending(p => p.InsertTime).ToList();
    //    }
    //    //tags_Search------------------------------------------
    //    //var tags = _context.Tags.Where(p => p.Name.Contains(request.SearchKey)).ToList();
    //    //foreach (var item in tags)
    //    //{
    //    //    var newsTag = _context.NewsInTags.Where(p => p.TagId == item.Id).ToList();
    //    //    foreach (var itemNewsTag in newsTag)
    //    //    {
    //    //     news = query.Pagenation(request.CurrentPage, request.PageSize).Where(p => p.Id == itemNewsTag.NewsId).Select(p =>
    //    //     new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
    //    //     {
    //    //         Id = p.Id,
    //    //         Title = p.Title,
    //    //         CategoryName = p.Category.Name,
    //    //         ShortDescription = p.ShortDescription,
    //    //         CategoryId = p.CategoryId,
    //    //         CoverNews = p.ImageUrl,
    //    //         UserName = p.User.FullName,
    //    //         InsertTime = p.InsertTime,
    //    //     }).OrderByDescending(p => p.InsertTime).ToList();
    //    //    }
    //    //}
    //}
    //if (request.CateId != 0)
    //{
    //    news = query.Pagenation(request.CurrentPage, request.PageSize).Where(p => p.CategoryId == request.CateId).Select(p =>
    //     new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
    //     {
    //         Id = p.Id,
    //         Title = p.Title,
    //         CategoryName = p.Category.Name,
    //         ShortDescription = p.ShortDescription,
    //         CategoryId = p.CategoryId,
    //         CoverNews = p.ImageUrl,
    //         UserName = p.User.FullName,
    //         InsertTime = p.InsertTime,
    //     }).OrderByDescending(p => p.InsertTime).ToList();
    //}
    //if (request.TagId != 0)
    //{
    //    var newsTag = _context.NewsInTags.Where(p => p.TagId == request.TagId).ToList();
    //    foreach (var item in newsTag)
    //    {
    //        news = query.Pagenation(request.CurrentPage, request.PageSize).Where(p => p.Id == item.NewsId).Select(p =>
    //     new ResultGetNewsSearchAndTagAndCategoryForSiteDto()
    //     {
    //         Id = p.Id,
    //         Title = p.Title,
    //         CategoryName = p.Category.Name,
    //         CategoryId = p.CategoryId,
    //         ShortDescription = p.ShortDescription,
    //         CoverNews = p.ImageUrl,
    //         UserName = p.User.FullName,
    //         InsertTime = p.InsertTime,
    //     }).OrderByDescending(p => p.InsertTime).ToList();
    //    }
    //}
    #endregion
}
