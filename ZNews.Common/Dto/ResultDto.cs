using System;
using System.Collections.Generic;
using System.Text;

namespace ZNews.Common.Dto
{
    public class ResultDto
    {
        public bool IsSuccess {get; set; }
        public string Message { get; set; }
    }
    public class ResultDto<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
