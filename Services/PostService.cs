using BlogPost.Data;
using BlogPost.DTOs;
using BlogPost.Exceptions;
using BlogPost.Models;
using BlogPost.PostRequests;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Services;

public class PostService(IPostRepository postRepository, ILogger<PostService> logger) : IPostService {

    private const string FieldRequiredMessage = "This field is required";
    private const string TitleConstraint = "IX_Post_Title";
    private readonly IPostRepository _postRepository = postRepository;
    private readonly ILogger _logger = logger;

    public IQueryable<Post> FindAllByPageRequest(PostPageRequest pageRequest){
            if (string.IsNullOrEmpty(pageRequest.SortBy)) pageRequest.SortBy = "date_desc";
            if (!pageRequest.NextPage) pageRequest.SortBy = pageRequest.SortBy == "date_desc" ? "date_asc" : "date_desc";
            var posts = _postRepository.FindAllByPredicate(pageRequest);
            return posts;
        }

    public async Task<List<Post>> FindAllByUserName(string userName) => await _postRepository.FindAllByUserName(userName);
    public Task<Post?> FindById(Guid? id) => _postRepository.FindById(id);
    public List<Topic> FindAllTopics() => _postRepository.FindAllTopics();

    public async Task CreatePost(Post post, List<CheckBox> topics)
    {
        ValidatePost(post, topics);
        var topicIds = topics.Where(x => x.IsChecked).Select(x => x.ItemId);
        var topicsPosts = topicIds.Select(topicId => new TopicsPosts(){TopicId = Guid.Parse(topicId), PostId = post.PostId}).ToList(); //wrong warning again validated before this line
        post.TopicsPosts =  topicsPosts;
        post.CreationDate = DateTime.Now;
        try {
            await _postRepository.CreatePost(post);
        }catch (Exception exception){
             _logger.LogWarning("Database Access issue, reason: {DT}", exception.Message);
            var dbUpdateException = (DbUpdateException) exception;
            if (dbUpdateException.InnerException is not null)
            {
              Dictionary<string, string> fieldErrors = [];
              var message = dbUpdateException.InnerException.Message;
              if (message.Contains(TitleConstraint)){
                fieldErrors.Add("Post.Title", "Title already exists");
                throw new RequestFieldInvalidException(fieldErrors, "Database Constraint Violated");
              }
            }
        }
    }

    private static void ValidatePost(Post post,  List<CheckBox> topics)
    {
        Dictionary<string, string> fieldErrors = [];
        if (IsTopicsInvalid(topics)) fieldErrors.Add("Topics", "Pick at least one topic");
        FieldRequired(post.Title,"Post.Title", fieldErrors);        //unrealistic warning here.. 
        FieldRequired(post.Content,"Post.Content", fieldErrors);
        FieldRequired(post.ThumbnailImagePath,"Post.ThumbnailImagePath", fieldErrors);
        if (fieldErrors.Count > 0) {
            throw new RequestFieldInvalidException(fieldErrors, "Invalid Request Data");
        }
    }

    public List<CheckBox> ReturnRelatedTopics (Guid? id){
     return  _postRepository.FetchRelatedPostTopics(id)
     .Select(x => new CheckBox(){Name = x.Name, ItemId = x.TopicId.ToString() , IsChecked = x.IsRelated == 1 ? true : false }).ToList();
    }

    public async Task UpdatePost(List<CheckBox> topics, Guid postId)
    {
        var post = await FindById(postId) ?? throw new NotFoundException("Post Does Not Exist");
        ValidatePost(post, topics);
        var topicsToAddIds = topics.Where(topic => topic.IsChecked && !TopicPostExist(Guid.Parse(topic.ItemId), post.PostId)).Select(x => x.ItemId);
        var TopicsToRemoveIds = topics.Where(x => !x.IsChecked).Select(x => x.ItemId);
        await _postRepository.UpdatePost(TopicsToRemoveIds, topicsToAddIds, post);
    }

    private static void FieldRequired(string fieldValue,string fieldName, Dictionary<string, string> fieldErrors)
    {
        if (string.IsNullOrEmpty(fieldValue)) fieldErrors.Add(fieldName, FieldRequiredMessage);
    }

    private bool TopicPostExist(Guid TopicId, Guid PostId) => _postRepository.TopicPostExist(TopicId, PostId);
    public async Task DeleteById(Guid PostId) => await _postRepository.DeleteById(PostId);
    private static bool IsTopicsInvalid(List<CheckBox> topics) {
        return topics is null || !topics.Where(topic => topic.IsChecked).Any();
    }
}