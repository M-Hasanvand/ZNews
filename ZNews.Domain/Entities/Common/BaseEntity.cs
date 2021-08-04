using System;
using System.Collections.Generic;
using System.Text;

namespace ZNews.Domain.Entities.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public DateTime? RemoveTime { get; set; }
        public bool IsRemove { get; set; }

    }
}
