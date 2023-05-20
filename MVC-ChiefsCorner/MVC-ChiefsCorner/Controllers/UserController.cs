using ChiefsCorner.Management.Service.Models;
using ChiefsCorner.Management.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC_ChiefsCorner.Models;
using MVC_ChiefsCorner.Models.Authentication.SignIn;
using MVC_ChiefsCorner.Models.Authentication.SignUp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            IActionResult viewResult = null;

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


                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var addingResult = await _userManager.CreateAsync(newUser, appUser.Password);

                    if (addingResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, role);


                        //TODO: (ACTIVATION CODE - 2. Madde)
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                        var emailDogrulamaLinki = Url.Action(nameof(ConfirmEmail), "User", new { token, email = newUser.Email });

                        var emailDogrulamaMesaji = new MailMessage(new Dictionary<string, string>() { { newUser.UserName!, newUser.Email! } }, "Email Doğrulama Linki", $"<b>Uygulamamıza giriş yapabilmeniz için doğrulama linki:</b> {emailDogrulamaLinki!}");
                        _emailService.SendEmail(emailDogrulamaMesaji);


                        viewResult = RedirectToAction("Index");
                    }
                    else
                    {
                        //Kullanıcı sisteme kayıt edilirken sunucuda hata oluştu!
                        //viewResult = StatusCode(StatusCodes.Status500InternalServerError, new AppResponse() { Status = "HATA OLUŞTU!", Message = "Kullanıcı sisteme kayıt edilirken sunucuda hata oluştu!" });
                        ModelState.AddModelError("PhoneNumber", "This number is already exist!");
                        viewResult = View(appUser);
                    }
                }
                else
                {
                    viewResult = StatusCode(StatusCodes.Status400BadRequest, new AppResponse() { Status = "HATA OLUŞTU!", Message = "Uygulamada böyle bir kullanıcı rolü bulunmamaktadır!" });
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
            IActionResult viewResult;

            var userByName = await _userManager.FindByNameAsync(appUser.Username);

            if (userByName != null)
            {

                if (await _userManager.CheckPasswordAsync(userByName, appUser.Password))
                {
                    if (await _userManager.IsEmailConfirmedAsync(userByName))
                    {
                        //TOKEN GENERATION
                        //TOKEN DISPLAY

                        /*var payload = new List<Claim>() { new Claim(ClaimTypes.Name,appUser.Username),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())};*/

                        var payload = new List<Claim>() { new Claim("username",appUser.Username),
                    new Claim("tokenID",Guid.NewGuid().ToString())};


                        //KULLANICININ ROLLERİ DE CLAIM'LERE EKLENİYOR. AUTHORIZATION ESNASINDA BU BİLGİ TOKEN GEÇERLİ İSE KULLANILACAK.
                        var userRoles = await _userManager.GetRolesAsync(userByName);
                        foreach (var userRole in userRoles)
                        {
                            payload.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        //3 sa 15 dk sonra yok olacak bir JWT üreteceğiz.
                        var jwtToken = GetToken(payload, 3, 15);
                        var tokenStr = new JwtSecurityTokenHandler().WriteToken(jwtToken);


                        viewResult = RedirectToAction("Index", "User");
                        //viewResult = Ok(new { token = tokenStr, expires = jwtToken.ValidTo });
                    }
                    else
                    {

                        viewResult = View(appUser);
                        //viewResult = StatusCode(StatusCodes.Status403Forbidden, new AppResponse()
                        //{
                        //    Status = "Giriş başarısız!",
                        //    Message = "Email onayı olmadan giriş yapamazsınız!"
                        //});
                    }
                }
                else
                {
                    viewResult = StatusCode(StatusCodes.Status406NotAcceptable, new AppResponse() { Status = "PAROLA HATASI!", Message = "Girilen parola hatalıdır!" });
                }
            }
            else
            {
                viewResult = StatusCode(StatusCodes.Status406NotAcceptable, new AppResponse() { Status = "KULLANICI ADI HATASI!", Message = "Sistemde böyle bir kullanıcı adı bulunmamaktadır!" });
            }


            return viewResult;
        }

        public IActionResult Logout()
        {
            // Kullanıcıyı çıkış yapmak için Identity yöneticisine işlemi bildirin
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private JwtSecurityToken GetToken(List<Claim> payload, int hours, int minutes)
        {
            //Sunucu tarafındaki JWT:Secret
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["Secret"]));

            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                //Belirlenen saat ve dakika sonrasında token sonlanacaktır.
                expires: DateTime.UtcNow.AddHours(hours).AddMinutes(minutes),
                //notBefore: DateTime.UtcNow.AddMinutes(5),//5 dk sonra token aktif olacaktır.
                claims: payload,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
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
                    return StatusCode(StatusCodes.Status200OK, new AppResponse() { Status = "Onaylama başarılı!", Message = "Kullanıcının maili onaylandı!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new AppResponse() { Status = "Onaylama başarısız!", Message = "Kullanıcının token bilgisi yanlıştır!" });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, new AppResponse() { Status = "Kullanıcı sistemde bulunmamaktadır!", Message = "Kullanıcı bulunamadı!" });
            }
        }
    }
}



//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using MVC_ChiefsCorner.Models;
//using MVC_ChiefsCorner.Models.Authentication.SignIn;
//using MVC_ChiefsCorner.Models.Authentication.SignUp;
//using NETCore.MailKit.Core;

//namespace MVC_ChiefsCorner.Controllers
//{
//    [Authorize]
//    public class UserController : Controller
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly SignInManager<AppUser> _signInManager;
//        private readonly IEmailService _emailService;

//        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _emailService = emailService;
//        }

//        [AllowAnonymous]
//        public IActionResult SignUp()
//        {
//            return View();
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> SignUp(SignUpAppUser model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new AppUser { UserName = model.Email, Email = model.Email };
//                var result = await _userManager.CreateAsync(user, model.Password);
//                if (result.Succeeded)
//                {
//                    // E-posta onaylama tokeni oluştur
//                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { token, email = user.Email }, Request.Scheme);

//                    // E-posta gönderme işlemini burada gerçekleştirin
//                    //_emailService.SendEmail(user.Email, confirmationLink);
//                    await _emailService.SendAsync(user.Email, "Email Confirmation - Chef's Corner", $"<b>Please click the following button to confirm your e-mail<b>: <button>{confirmationLink}</button>");
//                    return RedirectToAction(nameof(RegisterConfirmation));
//                }
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }
//            return View(model);
//        }

//        [AllowAnonymous]
//        public IActionResult RegisterConfirmation()
//        {
//            return View();
//        }

//        [AllowAnonymous]
//        public async Task<IActionResult> ConfirmEmail(string token, string email)
//        {
//            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
//            {
//                return RedirectToAction(nameof(SignUp));
//            }

//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//            {
//                return RedirectToAction(nameof(SignUp));
//            }

//            var result = await _userManager.ConfirmEmailAsync(user, token);
//            if (result.Succeeded)
//            {
//                return RedirectToAction(nameof(EmailConfirmed));
//            }

//            return RedirectToAction(nameof(SignUp));
//        }

//        [AllowAnonymous]
//        public IActionResult EmailConfirmed()
//        {
//            return View();
//        }

//        [AllowAnonymous]
//        public IActionResult SignIn(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            return View();
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> SignIn(SignInAppUser model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
//                if (result.Succeeded)
//                {
//                    return RedirectToLocal(returnUrl);
//                }
//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//            }
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
//            return RedirectToAction(nameof(HomeController.Index), "Home");
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))

//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction(nameof(HomeController.Index), "Home");
//            }
//        }
//    }
//}
