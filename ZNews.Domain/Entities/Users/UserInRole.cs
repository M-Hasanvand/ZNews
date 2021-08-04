using ZNews.Domain.Entities.Common;

namespace ZNews.Domain.Entities.Users
{
    public class UserInRole : BaseEntity<long>
    {
        ///---------------------------Relation{User}-------------
        public virtual User User { get; set; }
        public long UserId { get; set; }
        ///---------------------------Relation{Role}-------------
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }
    }
}
