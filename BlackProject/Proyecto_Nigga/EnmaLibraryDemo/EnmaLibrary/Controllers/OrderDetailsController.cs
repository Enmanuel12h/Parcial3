using EnmaLibrary.Models;
using EnmaLibrary.Repositories;
using EnmaLibrary.Repositories.Books;
using EnmaLibrary.Repositories.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnmaLibrary.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly ICustomersRepository _customersRepository;

        public OrderDetailsController(
            IOrderDetailsRepository orderDetailsRepository,
            IOrdersRepository ordersRepository,
            IBooksRepository booksRepository,
            ICustomersRepository customersRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
            _customersRepository = customersRepository;
        }

        public async Task<IActionResult> Index(int orderId)
        {
            var orderDetails = await _orderDetailsRepository.GetOrderDetailsByOrderIdAsync(orderId);
            return View(orderDetails);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var books = await _booksRepository.GetAllBooksAsync();

            // Crear un diccionario de precios de libros
            var bookPrices = books.ToDictionary(b => b.Id, b => b.Price);
            ViewBag.BookPrices = bookPrices;

            // Convertir la lista de libros a SelectListItem
            var bookList = books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title // Suponiendo que el título del libro se encuentra en la propiedad Title
            });

            // Pasar la lista de SelectListItem a la ViewBag
            ViewBag.Books = new SelectList(bookList, "Value", "Text");

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetailsModel orderDetails)
        {
            try
            {
                // Calcular el total price
                orderDetails.TotalPrice = orderDetails.Quantity * orderDetails.UnitPrice;

                // Agregar el nuevo detalle de pedido
                await _orderDetailsRepository.AddAsync(orderDetails);

                TempData["message"] = "Detalle de pedido agregado correctamente.";

                // Redirigir a la acción Index para mostrar todos los detalles de pedido relacionados con el pedido
                return RedirectToAction("GetAllOrderDetailsByOrderId", "Orders", new { id = orderDetails.OrderId });
            }
            catch (Exception ex)
            {
                // Si ocurre algún error, mostrar un mensaje de error y volver a la vista de creación
                TempData["message"] = $"Error al agregar el detalle de pedido: {ex.Message}";
                return View(orderDetails);
            }
        }


        // Otras acciones como Edit, Delete, etc., pueden seguir un patrón similar a la acción Create

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orderDetail = await _orderDetailsRepository.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            var books = await _booksRepository.GetAllBooksAsync();

            // Crear un diccionario de precios de libros
            var bookPrices = books.ToDictionary(b => b.Id, b => b.Price);
            ViewBag.BookPrices = bookPrices;

            // Convertir la lista de libros a SelectListItem
            var bookList = books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title // Suponiendo que el título del libro se encuentra en la propiedad Title
            });

            ViewBag.Books = new SelectList(bookList, "Value", "Text", orderDetail.BookId);

            return View(orderDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderDetailsModel orderDetails)
        {
            if (id != orderDetails.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(orderDetails);
            }

            try
            {
                await _orderDetailsRepository.EditAsync(orderDetails);
                TempData["message"] = "Detalle de pedido actualizado correctamente.";
                return RedirectToAction("GetAllOrderDetailsByOrderId", "Orders", new { id = orderDetails.Id });
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error al actualizar el detalle de pedido: {ex.Message}";
                return View(orderDetails);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var orderDetail = await _orderDetailsRepository.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, OrderDetailsModel details)
        {
            if (id != details.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(details);
            }

            try
            {
                await _orderDetailsRepository.DeleteAsync(details.Id);
                TempData["message"] = "Detalle de pedido eliminado correctamente.";
                return RedirectToAction("GetAllOrderDetailsByOrderId", "Orders", new { id = details.OrderId });
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error al eliminar el detalle de pedido: {ex.Message}";
                return RedirectToAction("GetAllOrderDetailsByOrderId", "Orders", new { details.OrderId });
            }
        }



    }
}
