using Contracts;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task CreateAsync(RegisterUserDto? userDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(string? userId, UpdateUserDto? userDto, CancellationToken cancellationToken = default);
    }
}
