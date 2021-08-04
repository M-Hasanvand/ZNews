using System;
using System.Collections.Generic;
using System.Text;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Menues;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Domain.Entities.Users
{
    public class User:BaseEntity<long>
    {
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public string NationalNumber { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public bool IsOwner { get; set; }

        ///---------------------------Relation{User}-------------
        public long? OneUserId { get; set; }
        public virtual User OneUser { get; set; }
        ///---------------------------Relation{User}-------------
        public virtual ICollection<User> SubordinateUsers { get; set; }
        ///---------------------------Relation{UserInRole}-------------
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        ///---------------------------Relation{News}-------------
        public virtual ICollection<News> News { get; set; }
        ///---------------------------Relation{Comments}-------------
        public virtual ICollection<Comment> Comments { get; set; }
        ///---------------------------Relation{Categories}-------------
        public virtual ICollection<Category> Categories { get; set; }
        ///---------------------------Relation{Tags}-------------
        public virtual ICollection<Tag> Tags { get; set; }
        ///---------------------------Relation{Menus}-------------
        public virtual ICollection<Menu> Menus { get; set; }
        ///---------------------------Relation{Menus}-------------
        public virtual ICollection<ChildMenu> ChildMenus { get; set; }
    }
}
