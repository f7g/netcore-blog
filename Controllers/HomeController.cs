using Blog.Models;
using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller {
    // Global variables
    private BlogDbContext _context;

    // Constructor
    public HomeController(BlogDbContext context) {
        _context = context;
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
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
