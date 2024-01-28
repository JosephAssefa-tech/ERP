using System;
using System.Collections.Generic;

namespace Excellerent.Timesheet.Domain.Helpers
{
    public class PaginatedData<T> : List<T>
    {
        public PaginatedData(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            TotalRecord = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public int TotalRecord { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        /*
        public static async Task<PaginatedData<T>> CreatAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedData<T>(items, count, pageIndex, pageSize);
        }
        //*/
    }
}
