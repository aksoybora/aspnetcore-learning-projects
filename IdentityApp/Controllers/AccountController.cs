using System.IO.Pipelines;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Please confirm your e-mail.");
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Your account was locked!, Please try again after {timeLeft.Minutes} minute.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong password!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Was not foundend via this e-mail!");
                }
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // E-posta doğrulaması için tek kullanımlık bir token üretiyoruz.
                    // Kullanıcı bu token'ı içeren linke tıkladığında hesabı doğrulanacak.
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new { user.Id, token });

                    // Aşağıdaki satır, kullanıcıya e-posta doğrulama bağlantısını gönderir.
                    // Bağlantıya tıklandığında ConfirmEmail aksiyonuna yönlendirilir ve token doğrulanır.
                    await _emailSender.SendEmailAsync(user.Email, "Confirm Your Email", $"Please confirm your email by clicking the link: <a href='http://localhost:5107{url}'>Confirm Email</a>.");

                    TempData["message"] = "Please confirm your email via the link sent to your inbox.";
                    return RedirectToAction("Login", "Account");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if (Id == null || token == null)
            {
                TempData["message"] = "Invalid token!";
                return View();
            }

            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    TempData["message"] = "Your e-mail was confirmed.";
                    return RedirectToAction("Login", "Account");
                }
            }

            TempData["message"] = "User was not found!";
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        


        public IActionResult ForgotPassword()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                TempData["message"] = "Please enter your email address.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if(user == null)
            {
                TempData["message"] = "No account matches the provided email address.";
                return View();
            }

            // Aşağıdaki satır, parolayı sıfırlamak için tek kullanımlık bir token üretir.
            // Bu token, yalnızca kısa bir süre geçerlidir ve e-postadaki linke tıklandığında kimlik doğrulaması için kullanılır.
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var url = Url.Action("ResetPassword","Account", new { user.Id, token} );

            // Aşağıdaki satır, kullanıcıya parola sıfırlama bağlantısını e-posta ile gönderir.
            // Kullanıcı bu bağlantıya tıkladığında, oluşturduğumuz token ile birlikte ResetPassword sayfasına yönlendirilir.
            await _emailSender.SendEmailAsync(Email, "Password Reset", $"To reset your password, please click the link: <a href='http://localhost:5107{url}'>Reset Password</a>.");

            TempData["message"] = "You can reset your password using the link sent to your email address.";

            return View();

        }


    }
}