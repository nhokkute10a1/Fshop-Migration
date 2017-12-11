<<<<<<< HEAD
﻿using System.Collections.Generic;
using System.Linq;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
>>>>>>> d92b92fa7af8a47b653f5126dd2af1eaa1f77d7d

namespace FShop.WebApi.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }

        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }

        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
<<<<<<< HEAD
      //  public int MaxPage { set; get; }
=======
        public int MaxPage { set; get; }
>>>>>>> d92b92fa7af8a47b653f5126dd2af1eaa1f77d7d
        public IEnumerable<T> Items { set; get; }
    }
}