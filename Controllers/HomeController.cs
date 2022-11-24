using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller {
    [HttpGet]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult Post() {
        return View();
    }

    [HttpGet]
    public IActionResult Edit() {
        return View(new Post());
    }

    [HttpPost]
    public IActionResult Edit(Post post) {
        return RedirectToAction("Index");
    }
}
