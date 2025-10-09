using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : "Unknown"))
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : "Unknown"))
                .ForMember(dest => dest.YearsOld,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year));

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : "Unknown"))
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : "Unknown"));

            CreateMap<Author, AuthorDto>();
        }
    }
}
