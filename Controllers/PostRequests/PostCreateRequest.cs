using BlogPost.DTOs;

namespace BlogPost.PostRequests{
    public class PostCreateRequest{
      public PostRequest? Post {get; set;}
      public List<CheckBox>? Topics {get; set;}
    }

    public class PostRequest {
     public string? Title {get; set;} 
     public string? Content {get; set;}
     public string? ThumbnailImagePath {get; set;} 
    }
}