using System.Collections.Generic;

namespace ZNews.Application.Services.Users.Queries.GetUsers
{
    public class ResultPagingUserDto
    {
        public List<GetUsersDto> Users { get; set; } = new List<GetUsersDto>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int UserCount { get; set; }
    }
}
