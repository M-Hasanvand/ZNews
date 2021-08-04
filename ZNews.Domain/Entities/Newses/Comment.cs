using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Newses
{
    public class Comment : BaseEntity<long>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        //-----------------Relation-User---1-----------------------|
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        //-----------------Relation-Comment---Comments-----------------------|
        public long? ParentId { get; set; }
        public virtual Comment comment { get; set; }

        public virtual List<Comment> ChildComments { get; set; }
        //-----------------Relation-News-----1-&---------------------------|
        public virtual News News { get; set; }
        public long NewsId { get; set; }
    }
}
