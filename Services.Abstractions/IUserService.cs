using Contracts.User;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(RegisterUserDto? userDto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(string? userId, JsonPatchDocument<UpdateUserDto>? userDto, CancellationToken cancellationToken = default);
    }
}
