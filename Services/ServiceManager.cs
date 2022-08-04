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

        public ServiceManager(IUnitOfWork unitOfWork, IEncodeService encodeService, IMapper mapper, UserManager<User> userManager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, encodeService, mapper, userManager));
        }

        public IUserService UserService => _userService.Value;
    }
}
