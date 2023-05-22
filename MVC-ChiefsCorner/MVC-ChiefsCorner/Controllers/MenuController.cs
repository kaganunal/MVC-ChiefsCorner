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
        //TODO Yapılacak
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

        public async Task<IActionResult> AddToOrder(int id)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.Id == id);
            return View(menu);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToOrder(int menuId, Size size, int quantity, int[] selectedExtras)
        {
            var user = await _userManager.GetUserAsync(User);
            var selectedOrder = _context.Orders.Include(x => x.User).FirstOrDefault(x => x.User == user && x.OrderStatus == OrderStatus.Pending);
            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == menuId);

            if (user != null)
            {
                if (selectedOrder != null)
                {
                    if (menu != null)
                    {

                        var orderMenu = new OrderMenu
                        {
                            Menu = menu,
                            Size = size,
                            Quantity = quantity,
                            Order = selectedOrder,
                            OrderId = selectedOrder.Id,

                        };

                        _context.OrderMenus.Add(orderMenu);
                        selectedOrder.CalculateQuantity();
                        selectedOrder.CalculateOrderTotal();
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        return Content("Error: Menu not found.");
                    }
                }
                else
                {
                    var order = new Order
                    {
                        UserId = user.Id,
                        OrderStatus = OrderStatus.Pending,
                        OrderTime = DateTime.Now,
                        User = user,
                        OrderMenus = new List<OrderMenu>()
                    };

                    if (menu != null)
                    {
                        var orderMenu = new OrderMenu
                        {
                            Menu = menu,
                            Size = size,
                            Quantity = quantity,
                            Order = order,
                            OrderId = order.Id
                        };

                        order.OrderMenus.Add(orderMenu);

                    }

                    order.CalculateQuantity();
                    order.CalculateOrderTotal();
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Menu");
                }
            }
            else
            {
                return RedirectToAction("SignIn", "User");
            }
        }



        public async Task<IActionResult> UpdateOrder()
        {
            return View();
        }

        [HttpPost]
        // Siparişteki menüyü güncelleme
        public async Task<IActionResult> UpdateOrder(int orderMenuId, Size newSize, int orderId)
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
