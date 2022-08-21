using Contracts.FeedBack;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Abstractions
{
    public interface IFeedBackService
    {
        Task<IEnumerable<FeedBackDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<FeedBackDto>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<FeedBackDto?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(AddFeedBackDto? feedBackDto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int feedBackId, JsonPatchDocument? feedBackDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int feedBackId, CancellationToken cancellationToken = default);
    }
}
