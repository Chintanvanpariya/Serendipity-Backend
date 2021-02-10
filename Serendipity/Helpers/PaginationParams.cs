using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendipity.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        public int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
