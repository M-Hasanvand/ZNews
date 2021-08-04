using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Domain.Entities.Common;
using ZNews.Domain.Entities.Users;

namespace ZNews.Domain.Entities.Menues
{
    public class Menu:BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //-------------------Relation---ICollection<ChildMenu>-------------//
        public virtual ICollection<ChildMenu> Children { get; set; }
        //-------------------Relation---User-------------//
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
