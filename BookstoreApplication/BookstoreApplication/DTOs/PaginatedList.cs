namespace BookstoreApplication.DTOs
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber => PageIndex + 1;
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            Count = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex < TotalPages - 1;
    }
}