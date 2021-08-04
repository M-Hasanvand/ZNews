using System.Collections.Generic;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Menues
{
    public class ChildMenu : BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //--------------------Relaton--Menu-------------//
        public long ParentId { get; set; }
        public virtual Menu Menu { get; set; }
        //------------------------Relation--ICollection<ChildMenu_Category>-----------------------//
        public virtual ICollection<ChildMenu_Category>  ChildMenu_Categories { get; set; }
        //------------------------Relation--ICollection<ChildMenu_Tag>-----------------------//
        public virtual ICollection<ChildMenu_Tag>  ChildMenu_Tags { get; set; }
        //-------------------Relation---User-------------//
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }

}
