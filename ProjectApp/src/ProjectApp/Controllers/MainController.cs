using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Models;
using ProjectApp.Services;
using ProjectApp.ViewModels;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectApp.Controllers
{
    public class MainController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSend _emailSend;

        public MainController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSend emailSend)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSend = emailSend;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResults = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (loginResults.Succeeded)
                {
                    //(action name, controller name)
                    return RedirectToAction("Index", "LoggedIn");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Information");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Username,
                };
                var identityResults = await _userManager.CreateAsync(identityUser, model.Password);

                if (identityResults.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var callbackUrl = Url.Action("ConfirmEmail", "Main", new { userId = identityUser.Id, code = code}, protocol: HttpContext.Request.Scheme);
                    await _emailSend.SendEmailAsync(model.Username, "Confirm Account", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>Link</a>");
                    await _signInManager.SignInAsync(identityUser, isPersistent: false);
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error in Creating User");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Index", "Main");
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (!result.Succeeded) return View(model);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "LoggedIn");
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var confirmed = await _userManager.IsEmailConfirmedAsync(user);
                if (user == null || !(confirmed))
                {
                    return View("ForgotPasswordConfirmation");
                }
                //send email confirmation
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Main", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSend.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking this link: <a href='{callbackUrl}'>Link</a>");
            }
            return View(model);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }

        public async Task<IActionResult> TestEmail()
        {
            await _emailSend.SendEmailAsync("dwebb761@gmail.com", "Hi Mom", "Sending you my first SendGrid email used in my project I'm working on!");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View("ConfirmEmail");
        }
    }
}
