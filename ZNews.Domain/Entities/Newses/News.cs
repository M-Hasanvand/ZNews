using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Newses
{
    public class News : BaseEntity<long>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long Views { get; set; }
        public long Like { get; set; }
        public bool DisLike { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        //-----------------Relation-User-----1----------------------------|
        public virtual User User { get; set; }
        public long UserId { get; set; }
        //-----------------Relation-Category----1-------------------------|
        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        //-----------------Relation-NewsInTag----&-1------------------------|
        public virtual ICollection<NewsInTag> NewsInTages { get; set; }
        //-----------------Relation-Comments----&-1------------------------|
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
