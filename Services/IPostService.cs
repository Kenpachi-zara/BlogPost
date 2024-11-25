using BlogPost.DTOs;
using BlogPost.Models;
using BlogPost.PostRequests;

namespace BlogPost.Services; 

public interface IPostService  {
    IQueryable<Post> FindAllByPageRequest(PostPageRequest pageRequest);
    Task<List<Post>> FindAllByUserName(string userName);
    Task<Post?> FindById(Guid? id);
    List<Topic> FindAllTopics();
    Task CreatePost(Post post, PostCreateRequest postCreateRequest);
    List<CheckBox> ReturnRelatedTopics (Guid? id);
    Task UpdatePost(PostEditRequest postEditRequest);
    Task DeleteById(Guid postId);
}