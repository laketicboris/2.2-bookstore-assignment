namespace BookstoreApplication.Services
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
    }
}