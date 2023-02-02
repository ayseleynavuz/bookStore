using AutoMapper;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // BOOK
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
            // GENRE
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            // AUTHOR
            CreateMap<CreateAuthorModel, Author>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Trim()))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName == null ? null : src.MiddleName.Trim()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Trim()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date));       
            CreateMap<UpdateAuthorModel, Author>()
                .ForMember(dest => dest.FirstName, opt => 
                    {
                        opt.Condition(src => src.FirstName != default && src.FirstName?.Trim() != string.Empty);
                        opt.MapFrom(src => src.FirstName!.Trim());
                    })
                .ForMember(dest => dest.MiddleName, opt => 
                    { 
                        opt.Condition(src => src.MiddleName != null); 
                        opt.MapFrom(src => (src.MiddleName == string.Empty ? null : src.MiddleName!.Trim()));
                    })
                .ForMember(dest => dest.LastName, opt => 
                    {
                        opt.Condition(src => src.LastName != default && src.LastName?.Trim() != string.Empty);
                        opt.MapFrom(src => src.LastName!.Trim());
                    })
                .ForMember(dest => dest.DateOfBirth, opt => 
                    { opt.Condition(src => src.DateOfBirth != default); opt.MapFrom(src => src.DateOfBirth.Date); });
            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => NameConverter.ConvertToFullName(src.FirstName, src.MiddleName, src.LastName)))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));
            CreateMap<Author, AuthorsViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => NameConverter.ConvertToFullName(src.FirstName, src.MiddleName, src.LastName)))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));

        }
    }
}