using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public BookRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Where(o => o.Authors != null && o.Authors.Any(o => o.FirstName.ToLower() == author.ToLower() ||
                        o.LastName.ToLower() == author.ToLower() ||
                        o.FullName.ToLower() == author.ToLower()))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Where(o => o.Genre.ToLower() == genre.ToLower())
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Where(o => o.Name.ToLower() == bookName.ToLower())
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Where(o => o.Pages == pageNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Where(o => o.ReleaseDate == releaseDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            return await _dbContext.Books
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .FirstOrDefaultAsync(o => o.Id == bookId, cancellationToken);
        }

        public void Create(Book? book)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _dbContext.Books.Add(book);
        }

        public void Delete(Book? book)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _dbContext.Books.Remove(book);
        }

        public void Update(int bookId, Book? book)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.Id = bookId;
            _dbContext.Books.Update(book);
        }
    }
}
