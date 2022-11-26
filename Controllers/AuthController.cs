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
    public IActionResult SignIn() {
        return View(new SignInViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel signInViewModel) {
        await _signInManager.PasswordSignInAsync(signInViewModel.Username, signInViewModel.Password, false, false);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut() {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
