using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.Common
{
    public class PagedAndSortedResultRequestDto
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public string Sorting { get; set; } // Optional: Add default sorting if needed
    }

}
