using EnmaLibrary.Models;
using EnmaLibrary.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace EnmaLibrary.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsersModel user)
        {
            try
            {
                await _usersRepository.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsersModel user)
        {
            try
            {
                await _usersRepository.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(UsersModel user)
        {
            try
            {
                await _usersRepository.DeleteUserAsync(user.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
