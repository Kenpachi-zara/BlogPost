using BlogPost.Models;
using BlogPost.PostRequests;

namespace BlogPost.Data {
  public interface IPostRepository{
    IQueryable<Post> FindAllByPredicate(PostPageRequest pageRequest);
    Task<List<Post>> FindAllByUserName(string UserName);
    Task<Post?> FindById(Guid? id);
    Task CreatePost(Post post);
    Task UpdatePost(IEnumerable<string> IdsToRemove, IEnumerable<string> IdsToAdd, Post post);
    Task DeleteById(Guid PostId);
    List<Topic> FindAllTopics();
    IEnumerable<RelatedTopicsModel> FetchRelatedPostTopics(Guid? id);
    bool TopicPostExist(Guid TopicId, Guid PostId);
  }
}