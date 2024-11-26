using BlogPost.Data;
using BlogPost.DTOs;
using BlogPost.Exceptions;
using BlogPost.Models;
using BlogPost.PostRequests;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Services;

public class PostService(IPostRepository postRepository, ILogger<PostService> logger, IWebHostEnvironment webHostEnvironment) : IPostService {

    private const string FieldRequiredMessage = "This field is required";
    private const string TitleConstraint = "IX_Post_Title";
    private const string DateDesc = "date_desc";
    private const string DateAsc = "date_asc";
    private readonly IPostRepository _postRepository = postRepository;
    private readonly ILogger _logger = logger;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    public IQueryable<Post> FindAllByPageRequest(PostPageRequest pageRequest){
            if (string.IsNullOrEmpty(pageRequest.SortBy)) pageRequest.SortBy = DateDesc;
            if (!pageRequest.NextPage) pageRequest.SortBy = pageRequest.SortBy == DateDesc ? DateAsc : DateDesc;
            var posts = _postRepository.FindAllByPredicate(pageRequest);
            return posts;
    }

    public async Task<List<Post>> FindAllByUserName(string userName) => await _postRepository.FindAllByUserName(userName);
    public Task<Post?> FindById(Guid? id) => _postRepository.FindById(id);
    public List<Topic> FindAllTopics() => _postRepository.FindAllTopics();

    public async Task CreatePost(Post post, PostCreateRequest postCreateRequest)
    {
        IFormFile? imageFile = postCreateRequest.Post.ImageFile;
        ValidateCreatePost(post, postCreateRequest.Topics, imageFile, PostAction.CREATE);
        HandlePostFields(post, postCreateRequest, imageFile);
        await UploadImage(imageFile);
        try
        {
            await _postRepository.CreatePost(post);
        }
        catch (Exception exception)
        {
            _logger.LogWarning("Database Access issue, reason: {DT}", exception.Message);
            var dbUpdateException = (DbUpdateException)exception;
            if (dbUpdateException.InnerException is not null)
            {
                Dictionary<string, string> fieldErrors = [];
                var message = dbUpdateException.InnerException.Message;
                if (message.Contains(TitleConstraint))
                {
                    fieldErrors.Add("Post.Title", "Title already exists");
                    throw new RequestFieldInvalidException(fieldErrors, "Database Constraint Violated");
                }
            }
        }
    }

    private static void HandlePostFields(Post post, PostCreateRequest postCreateRequest, IFormFile? imageFile)
    {
        List<TopicsPosts> topicsPosts = CreateTopicPosts(post, postCreateRequest);
        post.TopicsPosts = topicsPosts;
        post.CreationDate = DateTime.Now;
        post.ThumbnailImagePath = imageFile.FileName;
    }

    private static List<TopicsPosts> CreateTopicPosts(Post post, PostCreateRequest postCreateRequest)
    {
        var topicIds = postCreateRequest.Topics.Where(x => x.IsChecked).Select(x => x.ItemId);
        var topicsPosts = topicIds.Select(topicId => new TopicsPosts() { TopicId = Guid.Parse(topicId), PostId = post.PostId }).ToList(); //wrong warning again validated before this line
        return topicsPosts;
    }

    public List<CheckBox> ReturnRelatedTopics (Guid? id){
     return  _postRepository.FetchRelatedPostTopics(id)
     .Select(x => new CheckBox(){Name = x.Name, ItemId = x.TopicId.ToString() , IsChecked = x.IsRelated == 1 ? true : false }).ToList();
    }

    public async Task UpdatePost(PostEditRequest postEditRequest)
    {
        IFormFile? imageFile = postEditRequest.Post.ImageFile;
        var topics = postEditRequest.Topics;
        var post = await FindById(Guid.Parse(postEditRequest.Post.PostId)) ?? throw new NotFoundException("Post Does Not Exist");
        ValidateEditPost(topics, imageFile, PostAction.EDIT);
        await ModifyEntity(postEditRequest, post, imageFile);
        var topicsToAddIds = topics.Where(topic => topic.IsChecked && !TopicPostExist(Guid.Parse(topic.ItemId), post.PostId)).Select(x => x.ItemId);
        var TopicsToRemoveIds = topics.Where(x => !x.IsChecked).Select(x => x.ItemId);
        await _postRepository.UpdatePost(TopicsToRemoveIds, topicsToAddIds, post);
    }

    private async Task ModifyEntity(PostEditRequest postEditRequest, Post post, IFormFile? imageFile)
    {
        post.Content = postEditRequest.Post.Content ?? post.Content;
        post.Title = postEditRequest.Post.Title ?? post.Title;
        if (imageFile is not null){
            await UploadImage(imageFile);
            post.ThumbnailImagePath = imageFile.FileName;
        }
    }

    private static void ValidateEditPost(List<CheckBox>? topics, IFormFile? imageFile, PostAction action)
    {
        Dictionary<string, string> fieldErrors = [];
        if (IsTopicsInvalid(topics)) fieldErrors.Add("Topics", "Pick at least one topic");
        ValidateImage(imageFile, action, fieldErrors);
        if (fieldErrors.Count > 0)
        {
            throw new RequestFieldInvalidException(fieldErrors, "Invalid Request Data");
        }
    }
    
    private static void ValidateCreatePost(Post post,  List<CheckBox> topics, IFormFile? imageFile, PostAction action)
    {
        Dictionary<string, string> fieldErrors = [];
        if (IsTopicsInvalid(topics)) fieldErrors.Add("Topics", "Pick at least one topic");
        ValidateImage(imageFile, action, fieldErrors);
        FieldRequired(post.Title, "Post.Title", fieldErrors);        
        FieldRequired(post.Content, "Post.Content", fieldErrors);
        if (fieldErrors.Count > 0)
        {
            throw new RequestFieldInvalidException(fieldErrors, "Invalid Request Data");
        }
    }

    private static void ValidateImage(IFormFile? imageFile, PostAction action, Dictionary<string, string> fieldErrors)
    {
        if (action.Equals(PostAction.CREATE))
        {
            if (imageFile is null) fieldErrors.Add("Post.ImageFile", FieldRequiredMessage);
            else if (!imageFile.ContentType.Equals("image/jpeg")) fieldErrors.Add("Post.ImageFile", "File type must be .jpg");
        }
        else if (imageFile is not null && !imageFile.ContentType.Equals("image/jpeg"))
            fieldErrors.Add("Post.ImageFile", "File type must be .jpg");
    }

    private async Task UploadImage(IFormFile? imageFile)
    {
        string webRootPath = _webHostEnvironment.WebRootPath;
        var fileName = imageFile.FileName;
        var path = Path.Combine(webRootPath, "images", fileName);
        using Stream fileStream = new FileStream(path, FileMode.Create);
        await imageFile.CopyToAsync(fileStream);
    }

    private static void FieldRequired(string? fieldValue,string fieldName, Dictionary<string, string> fieldErrors)
    {
        if (string.IsNullOrEmpty(fieldValue)) fieldErrors.Add(fieldName, FieldRequiredMessage);
    }

    private bool TopicPostExist(Guid TopicId, Guid PostId) => _postRepository.TopicPostExist(TopicId, PostId);
    public async Task DeleteById(Guid PostId) => await _postRepository.DeleteById(PostId);
    private static bool IsTopicsInvalid(List<CheckBox>? topics) {
        return topics is null || !topics.Where(topic => topic.IsChecked).Any();
    }
}