using System.Collections.Generic;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Menues;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Newses
{
    public class Tag: BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //-----------------Relation-User-------1-1------------------|
        public virtual User User { get; set; }
        public long UserId { get; set; }
        //-----------------Relation-NewsInTag----&-&------------------------|
        public virtual ICollection<NewsInTag> NewsInTages { get; set; }
        //------------------------Relation--ICollection<ChildMenu_Tag>-----------------------//
        public virtual ICollection<ChildMenu_Tag> ChildMenu_Tags { get; set; }
    }
}
