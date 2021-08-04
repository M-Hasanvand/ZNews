using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Domain.Entities.Common;

namespace ZNews.Domain.Entities.HomePage.Views
{
    public class HomePageView:BaseEntity<long>
    {
        public DateTime DateView { get; set; }
    }
}
