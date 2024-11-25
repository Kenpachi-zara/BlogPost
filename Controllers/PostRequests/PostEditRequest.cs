using BlogPost.DTOs;

namespace BlogPost.PostRequests{
    public class PostEditRequest{
      public EditRequest? Post {get; set;}
      public List<CheckBox>? Topics {get; set;}
    }

    public class EditRequest {
     public string? PostId {get; set;}
     public string? Title {get; set;} 
     public string? Content {get; set;} 
     public string? ThumbnailImagePath {get; set;} 
     public IFormFile? ImageFile {get; set;} 
    }
}