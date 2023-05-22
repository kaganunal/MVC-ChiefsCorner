using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;
using MVC_ChiefsCorner.Models.Authentication.SignUp;



namespace MVC_ChiefsCorner.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly ChiefsCornerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminUserController(ChiefsCornerContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string searchEmail, string searchName)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchEmail))
            {
                users = users.Where(u => u.Email.Contains(searchEmail));
                ViewBag.SearchEmail = searchEmail;
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                users = users.Where(u => u.UserName.Contains(searchName));
                ViewBag.SearchName = searchName;
            }

            var userList = users.Select(u => new AppUser
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToList();

            return View(userList);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([FromForm] SignUpAppUser signUpUser, string role)
        {
            if (ModelState.IsValid)
            {
                var userByName = await _userManager.FindByNameAsync(signUpUser.Username);
                var userByEmail = await _userManager.FindByEmailAsync(signUpUser.Email);
                var userByPhone = await _userManager.FindByEmailAsync(signUpUser.PhoneNumber);

                if (userByName != null)
                {
                    //İsim mevcut
                    ModelState.AddModelError("Username", "This username is already exist!");
                    return View(signUpUser);
                }
                else if (userByEmail != null)
                {
                    //Email mevcut
                    ModelState.AddModelError("Email", "This email is already exist!");
                    return View(signUpUser);
                }
                else if (userByPhone != null)
                {
                    //Numara mevcut
                    ModelState.AddModelError("PhoneNumber", "This number is already exist!");
                    return View(signUpUser);
                }
                else
                {
                    var user = new AppUser
                    {
                        UserName = signUpUser.Username,
                        Name = signUpUser.Name,
                        Surname = signUpUser.Surname,
                        Email = signUpUser.Email,
                        PhoneNumber = signUpUser.PhoneNumber,
                        BirthDate = signUpUser.BirthDate,
                        Gender = signUpUser.Gender
                    };

                    var result = await _userManager.CreateAsync(user, signUpUser.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role);

                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(signUpUser);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var editUser = new SignUpAppUser
            {
                Username = user.UserName,
                Name = user.Name!,
                Surname = user.Surname!,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            };

            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string userEmail, [FromForm] SignUpAppUser editUser)
        {
            if (userEmail != editUser.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = editUser.Username;
                user.Name = editUser.Name;
                user.Surname = editUser.Surname;
                user.Email = editUser.Email;
                user.PhoneNumber = editUser.PhoneNumber;
                user.BirthDate = editUser.BirthDate;
                user.Gender = editUser.Gender;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(editUser);
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var orders = _context.Orders.Where(o => o.UserId == id);
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // İşlem başarısız oldu, hata mesajı gösterilebilir.
                return View("Error");
            }
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
