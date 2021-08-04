using System;
using System.Collections.Generic;
using System.Text;
using ZNews.Application.Services.Users.Commands.EditProfileUser;
using ZNews.Application.Services.Users.Commands.LoginUserForAdmin;
using ZNews.Application.Services.Users.Commands.RegisterNewUserForAdmin;
using ZNews.Application.Services.Users.Commands.RemoveUserForAdmin;
using ZNews.Application.Services.Users.Commands.UserStatusChangeForAdmin;
using ZNews.Application.Services.Users.Queries.GetProfileUserForAdmin;
using ZNews.Application.Services.Users.Queries.GetRoles;
using ZNews.Application.Services.Users.Queries.GetUserProperties;
using ZNews.Application.Services.Users.Queries.GetUsers;
using ZNews.Application.Services.Users.Queries.GetUsersForUserActivity;

namespace ZNews.Application.InterFaces.FacadPatterns
{

    public interface IUserFacad
    {
        /// <summary>
        /// Commands
        /// </summary>
        RegisterUserForAdminService RegisterUserForAdminService { get; }
        UserStatusChangeForAdminService UserStatusChangeForAdminService { get; }
        RemoveUserForAdminService RemoveUserForAdminService { get; }
        EditProfileUserService EditProfileUserService { get; }
        LoginUserForAdminService LoginUserForAdminService { get; }
        /// <summary>
        /// Queries
        /// </summary>
        GetRolesService GetRolesService { get; }
        GetUsersService GetUsersService { get; }
        GetProfileUserForAdminService GetProfileUserForAdminService { get; }
        GetUserPropertiesService GetUserPropertiesService { get; }
        GetUsersForUserActivityService GetUsersForUserActivityService { get; }
    }
}
