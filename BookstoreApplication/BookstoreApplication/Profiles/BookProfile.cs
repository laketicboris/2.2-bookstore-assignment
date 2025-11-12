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
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year))
                .ForMember(dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.AverageRating));

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : "Unknown"))
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : "Unknown"));

            CreateMap<Author, AuthorDto>();

            CreateMap<RegistrationDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<ApplicationUser, ProfileDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<SaveIssueDto, Issue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.ReleaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.IssueNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.PageCount, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalApiId))
                .ForMember(dest => dest.CoverDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.IssueNumberString, opt => opt.MapFrom(src => src.IssueNumber.ToString()))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePath != null ?
                    new IssueImageDto { MediumUrl = src.ImagePath } : null))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.VolumeName != null ?
                    new IssueVolumeDto
                    {
                        Id = src.VolumeId,
                        Name = src.VolumeName,
                        ApiDetailUrl = src.VolumeApiDetailUrl
                    } : null)
                );

            CreateMap<NewReviewDto, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}