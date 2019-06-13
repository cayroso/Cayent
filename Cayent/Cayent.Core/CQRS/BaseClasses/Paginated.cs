using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public class Paginated<T>
    {
        public Paginated(List<T> list, int page, int pageSize, int itemCount)
        {
            Items = list;
            Page = page;
            PageSize = pageSize;
            ItemCount = itemCount;

            //  compute totalpages
            var remainder = itemCount % PageSize;
            var totalPages = itemCount / PageSize;

            if (remainder != 0)
                totalPages++;

            PageCount = totalPages;

            if (Page > PageCount)
                Page = PageCount;
        }

        public List<T> Items
        {
            protected set; get;
        }

        public int Page { protected set; get; }
        public int PageSize { protected set; get; }
        public int PageCount { protected set; get; }
        public int ItemCount { protected set; get; }

    }
}
