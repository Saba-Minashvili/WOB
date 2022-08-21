using Contracts.FeedBack;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class FeedBackRepository : IFeedBackRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public FeedBackRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<FeedBack>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            return await _dbContext.FeedBacks
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FeedBack>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            return await _dbContext.FeedBacks
                .Where(o => o.BookId == bookId)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<FeedBack?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            return await _dbContext.FeedBacks
                .FirstOrDefaultAsync(o => o.Id == feedBackId, cancellationToken);
        }

        public void CreateAsync(FeedBack? feedBack)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            if(feedBack == null)
            {
                throw new ArgumentNullException(nameof(feedBack));
            }

            _dbContext.FeedBacks.Add(feedBack);
        }

        public void DeleteAsync(FeedBack? feedBack)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            if (feedBack == null)
            {
                throw new ArgumentNullException(nameof(feedBack));
            }

            _dbContext.FeedBacks.Remove(feedBack);
        }

        public void UpdateAsync(FeedBack? feedBack, JsonPatchDocument? feedBackPatch)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            if(feedBack == null)
            {
                throw new NullReferenceException(nameof(feedBack));
            }

            feedBackPatch.ApplyTo(feedBack);
        }
    }
}
