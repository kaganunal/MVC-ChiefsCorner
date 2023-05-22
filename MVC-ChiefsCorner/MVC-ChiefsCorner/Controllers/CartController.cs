using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Controllers
{
    public class CartController : Controller
    {
        private readonly ChiefsCornerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(ChiefsCornerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // Geçerli kullanıcının siparişini alıyor
            var user = await _userManager.GetUserAsync(User);
            var selectedOrder = _context.Orders.Include(x => x.User)
                .FirstOrDefault(x => x.User == user && x.OrderStatus == OrderStatus.Pending);

            if (selectedOrder != null)
            {
                // Siparişteki menüleri alıyo
                var orderMenus = _context.OrderMenus
                    .Include(om => om.Menu)
                    .Where(om => om.OrderId == selectedOrder.Id)
                    .ToList();


                return View(orderMenus);
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(new List<OrderMenu>());
        }

        public async Task<IActionResult> CompleteOrder(int orderId)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var menus = _context.OrderMenus.Include(om => om.Order).ThenInclude(x => x.User).Include(o => o.Menu).Where(om => om.OrderId == orderId && om.Order.OrderStatus == OrderStatus.Pending && om.Order.User == user).ToList();

                var order = await _context.Orders
                .Include(om => om.OrderMenus)
                .FirstOrDefaultAsync(om => om.Id == orderId && om.UserId == user.Id && om.OrderStatus == OrderStatus.Pending);

                if (order != null)
                {
                    order.OrderTime = DateTime.Now;
                    order.OrderStatus = OrderStatus.Completed;
                    _context.Update(order);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Order has been completed successfully.";

                    return View(menus);
                }
            }

            TempData["ErrorMessage"] = "Order not found or user authentication failed.";

            return RedirectToAction("Index", "Cart");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int orderMenuId, int quantity, Size size)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var orderMenu = await _context.OrderMenus
                    .Include(om => om.Order)
                    .ThenInclude(o => o.User)
                    .FirstOrDefaultAsync(om => om.Id == orderMenuId && om.Order.User == user);

                if (orderMenu != null)
                {
                    if (quantity > 0)
                    {
                        orderMenu.Quantity = quantity;
                        orderMenu.Size = size;
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Quantity and size updated successfully.";
                    }
                    else
                    {
                        _context.OrderMenus.Remove(orderMenu);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Menu removed from cart successfully.";
                    }

                    return RedirectToAction("Index");
                }
            }

            TempData["ErrorMessage"] = "Failed to update quantity or menu not found.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveFromCart(int orderMenuId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var orderMenu = await _context.OrderMenus.Include(om => om.Order)
                                                         .ThenInclude(o => o.User)
                                                         .FirstOrDefaultAsync(om => om.Id == orderMenuId && om.Order.User == user);

                if (orderMenu != null)
                {
                    _context.OrderMenus.Remove(orderMenu);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Menu removed from cart successfully.";

                    return RedirectToAction("Index", "Cart");
                }
            }

            TempData["ErrorMessage"] = "Menu not found or user authentication failed.";

            return RedirectToAction("Index", "Cart");
        }
    }
}
