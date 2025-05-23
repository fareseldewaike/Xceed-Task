using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ViewModels;

namespace ProductCatalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager , UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("GetAllProducts", "Product");
                    }
                    else if (roles.Contains("Customer"))
                    {
                        return RedirectToAction("GetAllProductsWithStillInOffer", "Product");
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your login.");
                return View(model);
            }
        
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error occurred while logging out.");
            }
        }
    }
}
