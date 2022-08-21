namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }
        public IFavouriteBookRepository FavouriteBookRepository { get; }
        public IFeedBackRepository FeedBackRepository { get; }

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
