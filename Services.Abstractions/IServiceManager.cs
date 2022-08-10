namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IUserService? UserService { get; }
        IBookService? BookService { get; }
        IFavouriteBookService? FavouriteBookService { get; }
    }
}
