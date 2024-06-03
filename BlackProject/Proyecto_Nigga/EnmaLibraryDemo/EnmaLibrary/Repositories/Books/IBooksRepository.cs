using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Books
{
    public interface IBooksRepository
    {
        Task AddBookAsync(BooksModel book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<BooksModel>> GetAllBooksAsync();
        Task<BooksModel> GetBookByIdAsync(int id);
        Task UpdateBookAsync(BooksModel book);
    }
}