using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Data
{
    public class PagingSort
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        public string Order { get; set; }
        public string Contains { get; set; }
    }
}
