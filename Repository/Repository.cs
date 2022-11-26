using Blog.Data;
using Blog.Models;

namespace Blog.Repository;

public class Repository : IRepository {
    // Global variables
    private BlogDbContext _context;

    // Constructor
    public Repository(BlogDbContext context) {
        _context = context;
    }

    public void AddPost(Post post) {
        _context.Posts.Add(post);
    }

    public List<Post> GetAllPosts() {
        return _context.Posts.ToList();
    }

    public Post GetPost(int id) {
        var post = _context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return new Post();
        return post;
    }

    public void RemovePost(int id) {
        _context.Posts.Remove(GetPost(id));
    }

    public void UpdatePost(Post post) {
        _context.Posts.Update(post);
    }

    public async Task<bool> SaveChangesAsync() {
        if (await _context.SaveChangesAsync() > 0) return true;
        return false;
    }
}
