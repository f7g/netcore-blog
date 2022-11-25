using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

// IdentityDbContext has all the tables for identity users and roles
public class BlogDbContext : IdentityDbContext { 
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    // Create a table, and each row in that table is gonna be a Post object
    public DbSet<Post> Posts { get; set; }
}
