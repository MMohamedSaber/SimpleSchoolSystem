﻿using School.Core.Entities;

namespace School.Core.shared
{ 
   

        public class PaginatedResult<T>
        {
            public List<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

       
    }

