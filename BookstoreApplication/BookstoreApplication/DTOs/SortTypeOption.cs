using BookstoreApplication.Models;

namespace BookstoreApplication.DTOs
{
    public class SortTypeOption
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public SortTypeOption(PublisherSortType sortType)
        {
            Key = (int)sortType;
            Name = FormatSortTypeName(sortType);
        }

        private string FormatSortTypeName(PublisherSortType sortType)
        {
            return sortType switch
            {
                PublisherSortType.NameAscending => "Name (A → Z)",
                PublisherSortType.NameDescending => "Name (Z → A)",
                PublisherSortType.AddressAscending => "Address (A → Z)",
                PublisherSortType.AddressDescending => "Address (Z → A)",
                _ => sortType.ToString()
            };
        }
    }
}