using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService>? _userService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, mapper, userManager));
        }

        public IUserService UserService => _userService.Value;
    }
}
