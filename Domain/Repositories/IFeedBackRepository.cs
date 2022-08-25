using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Repositories
{
    public interface IFeedBackRepository
    {
        Task<List<FeedBack>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<FeedBack>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<FeedBack?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default);
        void Create(FeedBack? feedBack);
        void Update(FeedBack? feedBack, JsonPatchDocument? feedBackPatch);
        void Delete(FeedBack? feedBack);
    }
}
