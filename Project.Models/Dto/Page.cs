using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Dto
{
    public class Page<T> where T : class
    {
        public T Data { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }
    }
}
