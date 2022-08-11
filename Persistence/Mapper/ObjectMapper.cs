using AutoMapper;
using Contracts;
using Contracts.Book;
using Contracts.Genre;
using Contracts.User;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Mapper
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<User, RegisterUserDto>()
                .ReverseMap();
            CreateMap<User, UpdateUserDto>()
                .ReverseMap();
            CreateMap<Book, BookDto>()
                .ForMember(o => o.Authors, k => k.MapFrom(m => m.Authors))
                .ForMember(o => o.FeedBacks, k => k.MapFrom(m => m.FeedBacks))
                .ReverseMap();
            CreateMap<Book, AddBookDto>()
                .ReverseMap();
            CreateMap<FavouriteBook, FavouriteBookDto>()
                .ReverseMap();
            CreateMap<FavouriteBook, AddToFavouritesDto>()
                .ReverseMap();
            CreateMap<Author, AuthorDto>()
                .ReverseMap();
            CreateMap<FeedBack, FeedBackDto>()
                .ReverseMap();
            CreateMap<RoleModel, IdentityRole>()
                .ReverseMap();
            CreateMap<Genre, GenreDto>()
                .ReverseMap();
        }
    }
}
