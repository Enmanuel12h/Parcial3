using EnmaLibrary.Models;
using EnmaLibrary.Repositories.Books;
using Microsoft.AspNetCore.Mvc;

namespace EnmaLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<ActionResult> Index()
        {
            var books = await _booksRepository.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BooksModel book)
        {
            try
            {
                await _booksRepository.AddBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(book);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var book = await _booksRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BooksModel book)
        {
            try
            {
                await _booksRepository.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(book);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _booksRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(BooksModel book)
        {
            try
            {
                await _booksRepository.DeleteBookAsync(book.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}
