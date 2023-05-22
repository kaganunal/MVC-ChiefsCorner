using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMenuController : Controller
    {
        private readonly ChiefsCornerContext _context;

        public AdminMenuController(ChiefsCornerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var menuCategories = await _context.MenuCategories.ToListAsync();
            return View(menuCategories);
        }

        public async Task<IActionResult> Menu(int categoryId)
        {
            var menus = await _context.Menus.Where(m => m.MenuCategoryId == categoryId).ToListAsync();
            return View(menus);
        }

        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(MenuCategory category, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    category.ImagePath = "/images/" + fileName;

                }
                else
                {
                    category.ImagePath = "/images/categories/bowlsCategory.png";
                }

                _context.MenuCategories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, MenuCategory category, IFormFile? file)
        {
            if (id != category.Id)
            {
                TempData["ErrorMessage"] = "Invalid category ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        category.ImagePath = "/images/" + fileName;
                    }
                    else
                    {
                        var existingCategory = await _context.MenuCategories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == category.Id);
                        category.ImagePath = existingCategory.ImagePath;
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Category updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the category.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction(nameof(Index));
            }

            _context.MenuCategories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> DetailsCategory(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }




        //[ValidateAntiForgeryToken] niteliği, ASP.NET Core uygulamalarında Cross-Site Request Forgery(CSRF) saldırılarına karşı koruma sağlamak için kullanılır.CSRF saldırıları, yetkili bir kullanıcının tarayıcısını kötü niyetli bir web sitesine yönlendirerek, kullanıcının bilgilerini çalmak veya yetkili işlemler gerçekleştirmek amacıyla oturum açmasını sağlayan saldırılardır.

        //Bu nitelik, ASP.NET Core'un anti-CSRF mekanizmasını etkinleştirir. Bir form gönderildiğinde, bu nitelik otomatik olarak bir CSRF tokeni oluşturur ve form verileriyle birlikte gönderir. Sunucu tarafında, gelen isteğin token ile eşleşip eşleşmediği kontrol edilir. Eşleşme sağlanmazsa, istek reddedilir ve bir CSRF saldırısı olduğu düşünülür.
        public async Task<IActionResult> AddMenu()
        {
            ViewData["MenuCategories"] = await _context.MenuCategories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMenu(Menu menu, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    menu.ImagePath = "/images/" + fileName;
                }
                else
                {
                    menu.ImagePath = "/images/categories/bowlsCategory.png";
                }

                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MenuCategories"] = await _context.MenuCategories.ToListAsync();
            return View(menu);

        }

        public async Task<IActionResult> EditMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Menu not found.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["MenuCategories"] = await _context.MenuCategories.ToListAsync();
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenu(int id, Menu menu, IFormFile? file)
        {
            if (id != menu.Id)
            {
                TempData["ErrorMessage"] = "Invalid menu ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        menu.ImagePath = "/images/" + fileName;
                    }
                    else
                    {
                        var existingMenu = await _context.Menus.AsNoTracking().FirstOrDefaultAsync(a => a.Id == menu.Id);
                        menu.ImagePath = existingMenu.ImagePath;
                    }

                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Menu updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the menu.";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["MenuCategories"] = await _context.MenuCategories.ToListAsync();
            return View(menu);
        }

        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Menu not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMenuConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Menu not found.";
                return RedirectToAction(nameof(Index));
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Menu deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailsMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Menu not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(menu);
        }
    }
}
