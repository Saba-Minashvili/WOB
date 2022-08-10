using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default);
        Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
        void Create(Book? book);
        void Update(int bookId, Book? book);
        void Delete(Book? book);
    }
}
