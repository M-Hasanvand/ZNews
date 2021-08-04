using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Domain.Entities.Menues
{
    public class ChildMenu_Category:BaseEntity<long>
    {
        //--------------------Relaton--Menu-------------//
        public long ChildMenuId { get; set; }
        public virtual ChildMenu ChildMenu { get; set; }
        //--------------------Relaton--Category-------------//
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
