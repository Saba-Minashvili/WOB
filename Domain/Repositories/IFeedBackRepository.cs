using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Repositories
{
    public interface IFeedBackRepository
    {
        Task<IEnumerable<FeedBack>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<FeedBack>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<FeedBack?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default);
        void CreateAsync(FeedBack? feedBack);
        void UpdateAsync(FeedBack? feedBack, JsonPatchDocument? feedBackPatch);
        void DeleteAsync(FeedBack? feedBack);
    }
}
