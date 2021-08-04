namespace ZNews.Application.Services.Users.Queries.GetUsers
{
    public class RequestGetUsersDto
    {
        public string SearchKey { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
