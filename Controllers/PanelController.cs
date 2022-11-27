using Blog.Models;
using Blog.Data.Repository;
using Blog.ViewModels;
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
        if (id == null) return View(new PostViewModel());
        else {
            var post = _repo.GetPost((int) id);
            return View(new PostViewModel {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel vm) {
        var post = new Post {
            Id = vm.Id,
            Title = vm.Title,
            Body = vm.Body,
            Image = "", // To do store image from view model
        };

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
