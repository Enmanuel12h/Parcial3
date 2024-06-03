using EnmaLibrary.Models;
using EnmaLibrary.Repositories.Customers;
using Microsoft.AspNetCore.Mvc;

namespace EnmaLibrary.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<ActionResult> Index()
        {
            var customers = await _customersRepository.GetAllCustomersAsync();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomersModel customer)
        {
            try
            {
                await _customersRepository.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customersRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomersModel customer)
        {
            try
            {
                await _customersRepository.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customersRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CustomersModel customer)
        {
            try
            {
                await _customersRepository.DeleteCustomerAsync(customer.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }
    }
}
