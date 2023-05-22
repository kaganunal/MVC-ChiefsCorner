using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly ChiefsCornerContext _context;

        public AdminOrderController(ChiefsCornerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, OrderStatus? status, int page = 1, int pageSize = 5)
        {
            var orders = _context.Orders.AsQueryable();

            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.OrderTime.Date >= startDate.Value.Date);
                ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.OrderTime.Date <= endDate.Value.Date);
                ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            }

            if (status.HasValue)
            {
                orders = orders.Where(o => o.OrderStatus == status);
                ViewBag.Status = status;
            }


            var totalItems = await orders.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var skip = (page - 1) * pageSize;

            var filteredOrders = await orders.Skip(skip).Take(pageSize).ToListAsync();

            ViewBag.Orders = filteredOrders;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View();
        }


        public async Task<IActionResult> UpdateOrderStatus(int id)
        {
            var updatedStatus = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction(nameof(Index));
            }

            order.OrderStatus = status;
            _context.Update(order);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order status updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
