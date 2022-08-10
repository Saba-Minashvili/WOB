using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Encoder.Abstraction;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService>? _userService;
        private readonly Lazy<IBookService>? _bookService;
        private readonly Lazy<IFavouriteBookService>? _favouriteBookService;

        public ServiceManager(IUnitOfWork unitOfWork, IEncodeService encodeService, IMapper mapper, UserManager<User> userManager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, encodeService, mapper, userManager));
            _bookService = new Lazy<IBookService>(() => new BookService(unitOfWork, encodeService, mapper));
            _favouriteBookService = new Lazy<IFavouriteBookService>(() => new FavouriteBookService(unitOfWork, encodeService, mapper));
        }

        public IUserService UserService => _userService.Value;
        public IBookService BookService => _bookService.Value;
        public IFavouriteBookService FavouriteBookService => _favouriteBookService.Value;
    }
}
