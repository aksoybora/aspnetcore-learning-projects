using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        // Kullanıcı işlemleri için repository enjekte ediliyor
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
        public IActionResult Login()
        {
            // Login formunu gösterir
            if(User.Identity!.IsAuthenticated) // Oturum açılmışsa (cookie bilgisi varsa) direk index ana sayfasına yönlendirilir.
            {
                return RedirectToAction("Index","Posts");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);
                if(user == null)
                {
                    _userRepository.CreateUser(new User {
                        UserName  = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "avatar.jpg"
                    });
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Username ya da Email kullanımda.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)  
            {
                // Basit doğrulama: e-posta ve şifre eşleşmesini kontrol eder
                var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if(isUser != null)
                {
                    // Kullanıcıya ait claim listesi hazırlanır
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if (isUser.Email == "info@sadikturan.com")
                    {
                        // Örnek: belirli e-posta için admin rolünü ekle
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    } 

                    // Cookie kimliği oluşturulur
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties 
                    {
                        IsPersistent = true
                    };

                    // Önce varsa mevcut oturumu sonlandır
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    // Yeni oturum aç
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    // Başarılı giriş sonrası ana sayfaya yönlendir
                    return RedirectToAction("Index","Posts");
                }
                else
                {
                    // Hata mesajı göster
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            } 
            
            return View(model);
        }
       
       
    }
}