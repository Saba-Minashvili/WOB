﻿using Contracts.User;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(RegisterUserDto? userDto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(string? userId, UpdateUserDto? userDto, CancellationToken cancellationToken = default);
    }
}
