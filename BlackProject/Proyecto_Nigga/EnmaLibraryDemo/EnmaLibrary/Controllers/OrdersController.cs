using EnmaLibrary.Models;
using EnmaLibrary.Repositories;
using EnmaLibrary.Repositories.Customers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnmaLibrary.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICustomersRepository customersRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<ActionResult> GetAllOrderDetailsByOrderId(int id)
        {
            ViewBag.OrderId = id;
            var orderDetails = await _orderDetailsRepository.GetOrderDetailsByOrderIdAsync(id);
            return View(orderDetails);
        }

        public async Task<ActionResult> Index()
        {
            var orders = await _ordersRepository.GetAllAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var customers = await _customersRepository.GetAllCustomersAsync();

            // Convertir la lista de clientes a una lista de SelectListItem
            var customerList = customers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name}" // Modifica esto según la estructura de tus clientes
            });

            // Pasar la lista de SelectListItem a la ViewBag
            ViewBag.Customers = customerList;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _customersRepository.GetAllCustomersAsync();
                ViewBag.Customers = customers;
                return View(order);
            }

            try
            {
                int id = await _ordersRepository.AddAsync(order);
                TempData["message"] = "Order created successfully.";
                return RedirectToAction("GetAllOrderDetailsByOrderId", new { id });
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                var customers = await _customersRepository.GetAllCustomersAsync();
                ViewBag.Customers = customers;
                return View(order);
            }
        }



        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var customers = await _customersRepository.GetAllCustomersAsync();
            // Convertir la lista de modelos de clientes en una lista de SelectListItem
            var customerList = customers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name // Suponiendo que el nombre del cliente está en la propiedad Name
            });
            ViewBag.Customers = customerList;
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _customersRepository.GetAllCustomersAsync();
                // Convertir la lista de modelos de clientes en una lista de SelectListItem
                var customerList = customers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name // Suponiendo que el nombre del cliente está en la propiedad Name
                });
                ViewBag.Customers = customerList;
                return View(order);
            }

            try
            {
                await _ordersRepository.EditAsync(order);
                TempData["message"] = "Order edited successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                var customers = await _customersRepository.GetAllCustomersAsync();
                // Convertir la lista de modelos de clientes en una lista de SelectListItem
                var customerList = customers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name // Suponiendo que el nombre del cliente está en la propiedad Name
                });
                ViewBag.Customers = customerList;
                return View(order);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, OrdersModel order)
        {
            try
            {
                await _ordersRepository.DeleteAsync(id);
                TempData["message"] = "Order deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return View(order);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
