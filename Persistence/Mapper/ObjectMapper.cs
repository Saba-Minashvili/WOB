using AutoMapper;
using Contracts;
using Domain.Entities;

namespace Persistence.Mapper
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(o => o.FavouriteBooks, k => k.MapFrom(m => m.FavouriteBooks))
                .ReverseMap();
            CreateMap<User, RegisterUserDto>()
                .ReverseMap();
            CreateMap<User, UpdateUserDto>()
                .ReverseMap();
            CreateMap<Book, BookDto>()
                .ForMember(o => o.Authors, k => k.MapFrom(m => m.Authors))
                .ForMember(o => o.FeedBacks, k => k.MapFrom(m => m.FeedBacks))
                .ReverseMap();
            CreateMap<Author, AuthorDto>()
                .ReverseMap();
            CreateMap<FeedBack, FeedBackDto>()
                .ReverseMap();
        }
    }
}
