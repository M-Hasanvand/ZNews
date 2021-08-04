using System.Collections.Generic;
using ZNews.Domain.Entities.Common;

namespace ZNews.Domain.Entities.Users
{
    public class Role : BaseEntity<long>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        ///---------------------------[ICollection<UserInRole>]-------------
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
