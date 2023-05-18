using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Controllers
{
    public class MenuController : Controller
    {
        private readonly ChiefsCornerContext _context;
        public MenuController(ChiefsCornerContext context)
        {
            _context = context;
        }
        //Kategorileri Listeleme

        public IActionResult Index()
        {
            var menuCategories = _context.MenuCategories.ToList();
            return View(menuCategories);
        }

        public IActionResult MenuList(int categoryId)
        {
            var menus = _context.Menus.Where(m => m.MenuCategoryId == categoryId).ToList();
            return View(menus);
        }
        // Menüleri listeleme
        //public async Task<IActionResult> Index()
        //{
        //    var menus = await _context.Menus
        //        .Include(m => m.MenuCategory)
        //        .ToListAsync();

        //    return View(menus);
        //}

        public IActionResult MenuDetails(int id)
        {
            var menu = _context.Menus.FirstOrDefault(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        public async Task<IActionResult> AddToOrder()
        {
            return View();
        }

        [HttpPost]
        // Menüleri siparişe ekleme
        public async Task<IActionResult> AddToOrder(int menuId, int orderId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            var order = await _context.Orders
                .Include(o => o.OrderMenus)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (menu != null && order != null)
            {
                var orderMenu = new OrderMenu
                {
                    MenuId = menu.Id,
                    OrderId = order.Id,
                    Size = Size.Medium // Örnek olarak medium boyutunu varsayalım
                };

                order.OrderMenus.Add(orderMenu);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(OrderDetails), new { orderId });
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
