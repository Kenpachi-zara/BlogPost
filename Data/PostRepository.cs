using BlogPost.Models;
using BlogPost.PostRequests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Data;
public class PostRepository : IPostRepository{
  
    private readonly ApplicationContext _context;

    public PostRepository(ApplicationContext context)
     {
      _context = context;
     }

    public IQueryable<Post> FindAllByPredicate(PostPageRequest pageRequest){
        IQueryable<Post> posts = from p in _context.Post select p;
        posts = HandleFiltering(pageRequest, posts);
        posts = HandleSorting(pageRequest, posts);
        return posts;
    }

     private static IQueryable<Post> HandleFiltering(PostPageRequest pageRequest, IQueryable<Post> posts)
      {
       if (!string.IsNullOrEmpty(pageRequest.Title))
          posts = posts.Where(s => s.Title.Contains(pageRequest.Title));
       if (!string.IsNullOrEmpty(pageRequest.Author))
            posts = posts.Where(s => s.Author.Equals(pageRequest.Author));
       if (!string.IsNullOrEmpty(pageRequest.Topic))
             posts = posts.Where(p => p.TopicsPosts.Any(t => t.Topic.Name.Equals(pageRequest.Topic)));
          return posts;
      }

     private static IQueryable<Post> HandleSorting(PostPageRequest pageRequest, IQueryable<Post> posts)
      {
            if (pageRequest.SortBy.Equals("date_desc"))
                posts = posts.OrderByDescending(s => s.CreationDate);
            else
                posts = posts.OrderBy(s => s.CreationDate);
            return posts;
      }

    public async Task<List<Post>> FindAllByUserName(string userName)
    {           
        var posts = from p in _context.Post
        where p.Author == userName
        select p;
        return await posts.ToListAsync();         
    }

    public async Task<Post?> FindById(Guid? id) => await _context.Post.FirstOrDefaultAsync(m => m.PostId == id);

    public List<Topic> FindAllTopics() => _context.Topic.ToList(); // Should be in a different Repository but this is the only Action I need.

    public async Task CreatePost(Post post) { 
      await _context.AddAsync(post);
      await _context.SaveChangesAsync();
    }

    public IEnumerable<RelatedTopicsModel> FetchRelatedPostTopics(Guid? id) 
    {
       return _context.Database.SqlQueryRaw<RelatedTopicsModel>("EXEC RELATED_TOPICS @id", new SqlParameter("@id", id.ToString()));
    }

    public async Task UpdatePost(IEnumerable<string> idsToRemove, IEnumerable<string> idsToAdd, Post post){
        var topicsPostsToRemove = _context.TopicsPosts.Where(x => idsToRemove.Contains(x.TopicId.ToString()) && x.PostId.Equals(post.PostId));
        var topicsPostsToAdd = idsToAdd.Select(topicId => new TopicsPosts() { TopicId = Guid.Parse(topicId), PostId = post.PostId });
        post.ModifiedDate = DateTime.Now;
        _context.RemoveRange(topicsPostsToRemove); 
         await _context.AddRangeAsync(topicsPostsToAdd); 
        _context.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(Guid postId)
    {
        var post = await _context.Post.FindAsync(postId);
        if (post != null)
           _context.Post.Remove(post);
      await _context.SaveChangesAsync();
    }

    public bool TopicPostExist(Guid topicId, Guid postId) => _context.TopicsPosts.Any(a => topicId.Equals(a.TopicId) && postId.Equals(a.PostId));
}