using System.Collections.Generic;
using System.Text;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<ResultRoles>> Execute();
    }
}
