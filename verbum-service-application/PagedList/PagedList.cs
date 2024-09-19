using Microsoft.EntityFrameworkCore;

namespace verbum_service_infrastructure.PagedList
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int? page, int? pageSize)
        {
            var totalCount = 0;
            if (page.HasValue && pageSize.HasValue)
            {
                totalCount = query.Count();
                var items = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
                return new PagedList<T>(items, page.Value, pageSize.Value, totalCount);
            }
            var allItems = query.ToList();
            return new PagedList<T>(allItems, 0, 0, totalCount);
        }
    }
}
