namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }
        public IFavouriteBookRepository FavouriteBookRepository { get; }

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
