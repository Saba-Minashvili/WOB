using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public BookRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            return await _dbContext.Books
                .AsNoTracking()
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }))
                .ToListAsync(cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public async Task<List<Book>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .Where(o => o.Authors != null && o.Authors.Any(o => o.FirstName.ToLower() == author.ToLower() ||
                        o.LastName.ToLower() == author.ToLower() ||
                        o.FullName.ToLower() == author.ToLower()))
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }));
#pragma warning restore CS8604 // Possible null reference argument.

            return await books.ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .Where(o => o.Genres != null && o.Genres.Any(o => o.GenreName.ToLower() == genre.ToLower()))
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }));
#pragma warning restore CS8604 // Possible null reference argument.

            return await books.ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }))
                .Where(o => o.Name.ToLower() == bookName.ToLower());
#pragma warning restore CS8604 // Possible null reference argument.

            return await books.ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }))
                .Where(o => o.Pages == pageNumber);
#pragma warning restore CS8604 // Possible null reference argument.

            return await books.ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .SelectMany(o => o.Authors.Select(m => new Book
                {
                    Name = o.Name,
                    Image = o.Image,
                    Authors = new List<Author>()
                    {
                        new Author
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            BookId = m.BookId
                        }
                    },
                    Pages = o.Pages,
                    Description = o.Description,
                    ReleaseDate = o.ReleaseDate
                }))
                .Where(o => o.ReleaseDate == releaseDate);
#pragma warning restore CS8604 // Possible null reference argument.

            return await books.ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Books == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Books));
            }

            IQueryable<Book> books = _dbContext.Books
                .AsNoTracking()
                .Include(o => o.Authors)
                .Include(o => o.FeedBacks)
                .Include(o => o.Genres);

            return await books.FirstOrDefaultAsync(o => o.Id == bookId, cancellationToken);
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
