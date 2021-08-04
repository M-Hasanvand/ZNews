using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Domain.Entities.Menues
{
    public class ChildMenu_Tag : BaseEntity<long>
    {
        //--------------------Relaton--Menu-------------//
        public long ChildMenuId { get; set; }
        public virtual ChildMenu ChildMenu { get; set; }
        //--------------------Relaton--Menu-------------//
        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
