using ChiefsCorner.Management.Service.Models;
using ChiefsCorner.Management.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_ChiefsCorner.Models;
using MVC_ChiefsCorner.Models.Authentication.SignIn;
using MVC_ChiefsCorner.Models.Authentication.SignUp;

namespace MVC_ChiefsCorner.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] SignUpAppUser appUser, string role = "CUSTOMER")
        {
            ModelState.Remove("role");
            IActionResult viewResult = View();
            if (ModelState.IsValid)
            {

                var userByName = await _userManager.FindByNameAsync(appUser.Username);
                var userByEmail = await _userManager.FindByEmailAsync(appUser.Email);
                var userByPhone = await _userManager.FindByEmailAsync(appUser.PhoneNumber);

                if (userByName != null)
                {
                    //İsim mevcut
                    ModelState.AddModelError("Username", "This username is already exist!");
                    viewResult = View(appUser);
                }
                else if (userByEmail != null)
                {
                    //Email mevcut
                    ModelState.AddModelError("Email", "This email is already exist!");
                    viewResult = View(appUser);
                }
                else if (userByPhone != null)
                {
                    //Numara mevcut
                    ModelState.AddModelError("PhoneNumber", "This number is already exist!");
                    viewResult = View(appUser);
                }
                else
                {
                    var userRole = await _roleManager.FindByNameAsync(role);
                    if (userRole != null)
                    {
                        AppUser newUser = new()
                        {
                            UserName = appUser.Username,
                            Email = appUser.Email,
                            PhoneNumber = appUser.PhoneNumber,
                            Name = appUser.Name,
                            Surname = appUser.Surname,
                            BirthDate = appUser.BirthDate,
                            Gender = appUser.Gender,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };

                        var addingResult = await _userManager.CreateAsync(newUser, appUser.Password);

                        if (addingResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, role);

                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                            var emailDogrulamaLinki = Url.Action(nameof(ConfirmEmail), "User", new { token, email = newUser.Email });

                            var emailDogrulamaMesaji = new MailMessage(new Dictionary<string, string>() { { newUser.UserName!, newUser.Email! } }, "Email Doğrulama Linki", $"<b>Uygulamamıza giriş yapabilmeniz için doğrulama linki:</b> {emailDogrulamaLinki!}");
                            _emailService.SendEmail(emailDogrulamaMesaji);


                            viewResult = RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            viewResult = View(appUser);
                        }
                    }
                    else
                    {
                        viewResult = View(appUser);
                    }
                }

            }
            return viewResult;
        }
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInAppUser appUser)
        {
            IActionResult viewResult = View();
            if (ModelState.IsValid)
            {
                var userByName = await _userManager.FindByNameAsync(appUser.Username);
                if (userByName != null)
                {
                    var userCheckPassword = await _signInManager.CheckPasswordSignInAsync(userByName, appUser.Password, false);

                    if (userCheckPassword != null)
                    {
                        if (await _userManager.IsEmailConfirmedAsync(userByName))
                        {
                            if (userCheckPassword.Succeeded)
                            {
                                HttpContext.Session.SetString("UserName", userByName.UserName);
                                HttpContext.Session.SetString("UserId", userByName.Id);

                                await _signInManager.SignInAsync(userByName, false);


                                viewResult = RedirectToAction("Index", "Home");

                            }
                            else
                            {
                                viewResult = View(appUser);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Please confirm your email!";
                            viewResult = View();

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Your password is incorrect!");
                        viewResult = View(appUser);
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "This username does not exist!");
                    viewResult = View(appUser);
                }

            }
            else
            {
                viewResult = View(appUser);
            }


            return viewResult;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("SignIn", "User");
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("SignIn");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new SignUpAppUser
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            };

            return View(model);
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}

