namespace BlogPost.DTOs{
    public class PostViewDto{
     public Guid PostId {get; set;}
     public string? Title {get; set;} 
     public string? Content {get; set;} 
     public string? ThumbnailImagePath {get; set;} 
     public string? Author {get; set;}            //related to UserName -> AspNetUsers table. 
     public DateTime CreationDate {get; set;}     
     public DateTime? ModifiedDate {get; set;}     
    }
}