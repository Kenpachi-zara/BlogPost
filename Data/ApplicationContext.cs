using Microsoft.EntityFrameworkCore;
using BlogPost.Models;

namespace BlogPost.Data
{
    public class ApplicationContext : DbContext
    {
                
        public DbSet<Post> Post { get; set; } = default!;
        public DbSet<Topic> Topic { get; set; } = default!;
        public DbSet<TopicsPosts> TopicsPosts { get; set; } = default!;

        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Topic>().HasData(
                new Topic(){Name = "Tech", TopicId = Guid.NewGuid()},
                new Topic(){Name = "Science", TopicId = Guid.NewGuid()},
                new Topic(){Name = "Health", TopicId = Guid.NewGuid()},
                new Topic(){Name = "Religion", TopicId = Guid.NewGuid()},
                new Topic(){Name = "Life", TopicId = Guid.NewGuid()},
                new Topic(){Name = "Natural", TopicId = Guid.NewGuid()}
            );
            modelBuilder.Entity<TopicsPosts>().HasIndex(p => new { p.PostId, p.TopicId }).IsUnique();
            modelBuilder.Entity<Post>().HasIndex(p => p.Title).IsUnique();
        }
    }
}
