using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AuthController : Controller {
    // Global variabels
    private SignInManager<IdentityUser> _signInManager;

    // Constructor
    public AuthController(SignInManager<IdentityUser> signInManager) {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login() {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
        var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
        return RedirectToAction("Index", "Panel");
    }

    [HttpGet]
    public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
