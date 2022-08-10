using AutoMapper;
using Contracts.User;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Encoder.Abstraction;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IEncodeService? _encoder;
        private readonly IMapper? _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(
            IUnitOfWork? unitOfWork,
            IEncodeService encoder,
            IMapper? mapper, 
            UserManager<User> userManager) 
        {
            _unitOfWork = unitOfWork;
            _encoder = encoder;
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

            foreach(var user in usersDto)
            {
                user.Photo = _encoder.DecodeFromBase64(user.Photo);
            }

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
            userDto.Photo = _encoder.DecodeFromBase64(user.Photo);

            return userDto;
        }

        public async Task<bool> CreateAsync(RegisterUserDto? userDto, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(userDto);

            if (_unitOfWork == null)
            {
                throw new NullReferenceException(nameof(_unitOfWork));
            }

            if (_mapper == null)
            {
                throw new NullReferenceException(nameof(_mapper));
            }

            if(await CheckDuplicateEmailAsync(userDto.Email) == true)
            {
                throw new AlreadyExistsException("User with this email already exists.");
            }

            user.UserName = userDto.Email;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);

            IdentityResult result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(user, "User");

            return true;
        }

        public async Task<bool> UpdateAsync(string? userId, UpdateUserDto? userDto, CancellationToken cancellationToken = default)
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
            user.Photo = _encoder.EncodeToBase64(userDto.Photo);

            int result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        private async Task<bool> CheckDuplicateEmailAsync(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }
    }
}
