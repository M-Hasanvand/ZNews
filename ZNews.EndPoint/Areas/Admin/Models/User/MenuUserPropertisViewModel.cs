using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZNews.EndPoint.Areas.Admin.Models.User
{
    public class MenuUserPropertisViewModel
    {
        public Guid UserGuid { get; set; }
        public long UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}
