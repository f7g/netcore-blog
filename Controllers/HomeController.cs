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
        var posts = _repo.GetAllPosts();
        return View(posts);
    }

    [HttpGet]
    public IActionResult Post(int id) {
        var post = _repo.GetPost(id);
        return View(post);
    }
}
