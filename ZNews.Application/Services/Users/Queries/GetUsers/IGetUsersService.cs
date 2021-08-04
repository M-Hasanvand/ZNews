using System.Text;
using ZNews.Application.Services.Users.Commands.RegisterNewUserForAdmin;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultDto<ResultPagingUserDto> Execute(RequestGetUsersDto request);
    }
}
