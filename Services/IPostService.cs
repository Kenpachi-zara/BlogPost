using BlogPost.DTOs;
using BlogPost.Models;
using BlogPost.PostRequests;

namespace BlogPost.Services; 

public interface IPostService  {
    IQueryable<Post> FindAllByPageRequest(PostPageRequest pageRequest);
    Task<List<Post>> FindAllByUserName(string userName);
    Task<Post?> FindById(Guid? id);
    List<Topic> FindAllTopics();
    Task CreatePost(Post post, List<CheckBox> topics);
    List<CheckBox> ReturnRelatedTopics (Guid? id);
    Task UpdatePost(List<CheckBox> topics, Guid postId);
    Task DeleteById(Guid postId);
}