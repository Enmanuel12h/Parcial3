using EnmaLibrary.Data;
using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Books
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public BooksRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IEnumerable<BooksModel>> GetAllBooksAsync()
        {
            return await _dataAccess.GetDataAsync<BooksModel, dynamic>(
                "dbo.spBooks_GetAll",
                new { });
        }
        public async Task<BooksModel> GetBookByIdAsync(int id)
        {
            var books = await _dataAccess.GetDataAsync<BooksModel, dynamic>(
                "dbo.spBooks_GetById",
                new { Id = id });

            return books.FirstOrDefault();
        }

        public async Task AddBookAsync(BooksModel book)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spBooks_Insert",
                new { book.Title, book.Author, book.Price, book.Stock });
        }


        public async Task UpdateBookAsync(BooksModel book)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spBooks_Update",
                book);
        }
        public async Task DeleteBookAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spBooks_Delete",
                new { Id = id });
        }
    }
}
