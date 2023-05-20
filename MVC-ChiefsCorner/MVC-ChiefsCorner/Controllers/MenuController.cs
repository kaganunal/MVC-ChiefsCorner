using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Controllers
{
    public class MenuController : Controller
    {
        private readonly ChiefsCornerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MenuController(ChiefsCornerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Kategorileri Listeleme

        public async Task<IActionResult> Index()
        {
            var menuCategories = await _context.MenuCategories.ToListAsync();
            return View(menuCategories);
        }

        public async Task<IActionResult> MenuList(int categoryId)
        {
            var menus = await _context.Menus.Where(m => m.MenuCategoryId == categoryId).ToListAsync();
            return View(menus);
        }


        public IActionResult MenuDetails(int id)
        {
            var menu = _context.Menus.FirstOrDefault(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        public async Task<IActionResult> AddToOrder(Menu menu)
        {
            return View(menu);
        }

        [HttpPost]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> AddToOrder(int menuId, Size size)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == menuId);

                if (menu != null)
                {
                    var orderMenu = new OrderMenu
                    {
                        Menu = menu,
                        Size = size,
                    };

                    if (user.Orders == null)
                    {
                        user.Orders = new List<Order>();
                    }

                    var order = user.Orders.FirstOrDefault(); // Kullanıcının mevcut siparişini al veya yeni bir sipariş oluştur

                    if (order == null)
                    {
                        order = new Order
                        {
                            OrderTime = DateTime.Now,
                            User = user,
                            OrderMenus = new List<OrderMenu>()
                        };

                        user.Orders.Add(order);
                    }

                    order.OrderMenus.Add(orderMenu);

                    await _context.SaveChangesAsync();

                    return View(menu);
                }
                else
                {
                    return Content("Error: Menu not found.");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }



        [HttpPost]
        // Menüyü siparişten silme
        public async Task<IActionResult> RemoveFromOrder(int orderMenuId, int orderId)
        {
            var orderMenu = await _context.OrderMenus.FindAsync(orderMenuId);

            if (orderMenu != null)
            {
                _context.OrderMenus.Remove(orderMenu);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(OrderDetails), new { orderId });
        }

        public async Task<IActionResult> UpdateOrderMenu()
        {
            return View();
        }

        [HttpPost]
        // Siparişteki menüyü güncelleme
        public async Task<IActionResult> UpdateOrderMenu(int orderMenuId, Size newSize, int orderId)
        {
            var orderMenu = await _context.OrderMenus.FindAsync(orderMenuId);

            if (orderMenu != null)
            {
                orderMenu.Size = newSize;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(OrderDetails), new { orderId });
        }


        public async Task<IActionResult> OrderDetails()
        {
            return View();
        }

        // Sipariş detaylarını görüntüleme
        [HttpPost]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderMenus).ThenInclude(om => om.Menu)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return View(order);
        }
    }
}
