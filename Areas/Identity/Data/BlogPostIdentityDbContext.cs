using BlogPost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlogPost.Areas.Identity.Data;

public class BlogPostIdentityDbContext : IdentityDbContext<BlogPostUser>
{
    public BlogPostIdentityDbContext(DbContextOptions<BlogPostIdentityDbContext> options)
        : base(options)
    {
    }
    
      protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(GetRoles());
    }

    private List<IdentityRole> GetRoles()
    {
        var adminRole = new IdentityRole("Admin");
        adminRole.NormalizedName = adminRole.Name!.ToUpper();

        var postRole = new IdentityRole("Post");
        postRole.NormalizedName = postRole.Name!.ToUpper();

        List<IdentityRole> roles = new List<IdentityRole>() {
           adminRole,
           postRole
        };
        return roles;
    }
}
