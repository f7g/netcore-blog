using Blog.Data.Repository;
using Blog.Data.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller {
    // Global variables
    private IRepository _repo;
    private IFileManager _fileManager;

    // Constructor
    public HomeController(IRepository repo, IFileManager fileManager) {
        _repo = repo;
        _fileManager = fileManager;
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

    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image) {
        var imageType = image.Substring(image.LastIndexOf(".") + 1);
        return new FileStreamResult(_fileManager.ImageStream(image), $"image/{imageType}");
    }
}
