namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
