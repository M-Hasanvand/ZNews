using System;
using System.Collections.Generic;

namespace ZNews.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersDto
    {
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsOwner { get; set; }
        public List<RoleDto> RoleDto { get; set; }
    }
}
