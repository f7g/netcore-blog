using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller {
    // Global variables
    private IRepository _repo;

    // Constructor
    public HomeController(IRepository repo) {
        _repo = repo;
    }

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
    public async Task<IActionResult> Edit(Post post) {
        _repo.AddPost(post);
        if (await _repo.SaveChangesAsync())
            return RedirectToAction("Index");
        else
            return View(post);
    }
}
