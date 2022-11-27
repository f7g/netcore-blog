using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Authorize(Roles = "Admin")] // Authorize first before you reach the controller
public class PanelController : Controller { 
    // Global variables
    private IRepository _repo;

    // Constructor
    public PanelController(IRepository repo) {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Index() {
        var posts = _repo.GetAllPosts();
        return View(posts);
    }

    [HttpGet]
    public IActionResult Edit(int? id) {
        if (id == null) return View(new Post());
        else {
            var post = _repo.GetPost((int) id);
            return View(post);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Post post) {
        if (post.Title == null) return View(new Post());
        if (post.Body == null) return View(new Post());

        if (post.Id > 0) _repo.UpdatePost(post);
        else _repo.AddPost(post);
        
        if (await _repo.SaveChangesAsync()) return RedirectToAction("Index");
        else return View(post);
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id) {
        _repo.RemovePost(id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
