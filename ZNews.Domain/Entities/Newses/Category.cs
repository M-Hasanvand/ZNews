using System.Collections.Generic;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Menues;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Newses
{
    public class Category:BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //-----------------Relation-User---1-1----------------------|
        public virtual User User { get; set; }
        public long UserId { get; set; }
        //------------------------Relation--ICollection<ChildMenu_Category>-----------------------//
        public virtual ICollection<ChildMenu_Category> ChildMenu_Categories { get; set; }
    }
}
