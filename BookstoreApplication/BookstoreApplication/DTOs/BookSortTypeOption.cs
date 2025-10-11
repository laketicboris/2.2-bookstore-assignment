using BookstoreApplication.Models;

namespace BookstoreApplication.DTOs
{
    public class BookSortTypeOption
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public BookSortTypeOption(BookSortType sortType)
        {
            Key = (int)sortType;
            Name = FormatSortTypeName(sortType);
        }

        private string FormatSortTypeName(BookSortType sortType)
        {
            return sortType switch
            {
                BookSortType.TitleAscending => "Title (A → Z)",
                BookSortType.TitleDescending => "Title (Z → A)",
                BookSortType.PublishedDateAscending => "Published Date (Oldest First)",
                BookSortType.PublishedDateDescending => "Published Date (Newest First)",
                BookSortType.AuthorNameAscending => "Author Name (A → Z)",
                BookSortType.AuthorNameDescending => "Author Name (Z → A)",
                _ => sortType.ToString()
            };
        }
    }
}