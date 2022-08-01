using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork? unitOfWork, IMapper? mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_unitOfWork == null)
            {
                throw new NullReferenceException(nameof(_unitOfWork));
            }

            if(_mapper == null)
            {
                throw new NullReferenceException(nameof(_mapper));
            }

            var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_unitOfWork == null)
            {
                throw new NullReferenceException(nameof(_unitOfWork));
            }

            if (_mapper == null)
            {
                throw new NullReferenceException(nameof(_mapper));
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<RegisterUserDto?> CreateAsync(RegisterUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if (_unitOfWork == null)
            {
                throw new NullReferenceException(nameof(_unitOfWork));
            }

            if (_mapper == null)
            {
                throw new NullReferenceException(nameof(_mapper));
            }

            var user = _mapper.Map<User>(userDto);

            user.UserName = userDto.Email;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);

            await _userManager.CreateAsync(user);

            return userDto;
        }

        public async Task UpdateAsync(string? userId, UpdateUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if (_unitOfWork == null)
            {
                throw new NullReferenceException(nameof(_unitOfWork));
            }

            if (_mapper == null)
            {
                throw new NullReferenceException(nameof(_mapper));
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Photo = userDto.Photo;

            await _unitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
