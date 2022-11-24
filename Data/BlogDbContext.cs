using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class BlogDbContext : DbContext { 
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    // Create a table, and each row in that table is gonna be a Post object
    public DbSet<Post> Posts { get; set; }
}
