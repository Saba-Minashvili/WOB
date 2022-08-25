using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default);
        void Create(User user);
        void Update(User? user, JsonPatchDocument? userPatch);
    }
}
