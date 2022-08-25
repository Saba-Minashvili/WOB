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

        public async Task<List<FeedBack>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            return await _dbContext.FeedBacks
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FeedBack>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            IQueryable<FeedBack> feedBacks = _dbContext.FeedBacks
                .AsNoTracking()
                .Where(o => o.BookId == bookId);

            return await feedBacks.ToListAsync(cancellationToken);
        }
        
        public async Task<FeedBack?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            return await _dbContext.FeedBacks
                .AsQueryable()
                .FirstOrDefaultAsync(o => o.Id == feedBackId, cancellationToken);
        }

        public void Create(FeedBack? feedBack)
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

        public void Delete(FeedBack? feedBack)
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

        public void Update(FeedBack? feedBack, JsonPatchDocument? feedBackPatch)
        {
            if (_dbContext.FeedBacks == null)
            {
                throw new NullReferenceException(nameof(_dbContext.FeedBacks));
            }

            if (feedBack == null)
            {
                throw new NullReferenceException(nameof(feedBack));
            }

            if(feedBackPatch == null)
            {
                throw new ArgumentNullException(nameof(feedBackPatch));
            }

            feedBackPatch.ApplyTo(feedBack);
        }
    }
}
