using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Book>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default);
        Task<List<Book>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default);
        Task<List<Book>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default);
        Task<List<Book>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<List<Book>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default);
        Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
        void Create(Book? book);
        void Update(int bookId, Book? book);
        void Delete(Book? book);
    }
}
