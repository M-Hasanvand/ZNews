using System;
using Microsoft.AspNetCore.Hosting;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
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

namespace ZNews.Application.Services.Users.FacadPattern
{
    public class UserFacadForAdmin:IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public UserFacadForAdmin(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private  RegisterUserForAdminService  _registerUserForAdminService;
        public RegisterUserForAdminService RegisterUserForAdminService => _registerUserForAdminService ?? new RegisterUserForAdminService(_context);
        
        private  UserStatusChangeForAdminService _userStatusChangeForAdminService;
        public UserStatusChangeForAdminService UserStatusChangeForAdminService => _userStatusChangeForAdminService ?? new UserStatusChangeForAdminService(_context);
        
        private  RemoveUserForAdminService _removeUserForAdminService;
        public RemoveUserForAdminService RemoveUserForAdminService => _removeUserForAdminService ?? new RemoveUserForAdminService(_context,_environment);

        private EditProfileUserService _editProfileUserService;
        public EditProfileUserService EditProfileUserService => _editProfileUserService ?? new EditProfileUserService(_context, _environment);
        
        private LoginUserForAdminService _loginUserForAdminService;
        public LoginUserForAdminService LoginUserForAdminService => _loginUserForAdminService ?? new LoginUserForAdminService(_context);
        /// <summary>
        /// Queries
        /// </summary>
        private GetRolesService _getRolesService;
        public  GetRolesService GetRolesService=>_getRolesService??new GetRolesService(_context);

        private GetUsersService _qetUsersService;
        public GetUsersService GetUsersService => _qetUsersService ?? new GetUsersService(_context);

        private GetProfileUserForAdminService _getProfileUserForAdminService;
        public GetProfileUserForAdminService GetProfileUserForAdminService => _getProfileUserForAdminService ?? new GetProfileUserForAdminService(_context);

        private GetUserPropertiesService _getUserPropertiesService;
        public GetUserPropertiesService GetUserPropertiesService => _getUserPropertiesService ?? new GetUserPropertiesService(_context);
        
        private GetUsersForUserActivityService _getUsersForUserActivityService;
        public GetUsersForUserActivityService GetUsersForUserActivityService => _getUsersForUserActivityService ?? new GetUsersForUserActivityService(_context);

    }
}
