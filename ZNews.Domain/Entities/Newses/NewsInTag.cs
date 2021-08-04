using ZNews.Domain.Entities.Common;

namespace ZNews.Domain.Entities.Newses
{
    public class NewsInTag:BaseEntity<long>
    {
        //-----------------Relation-News---1-1----------------------|
        public virtual News News { get; set; }
        public long NewsId { get; set; }
        //-----------------Relation-Tag---1-1----------------------|
        public virtual Tag Tag { get; set; }
        public long TagId { get; set; }
    }
}
