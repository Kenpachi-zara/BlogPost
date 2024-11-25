
namespace BlogPost.DTOs
{
    public class PostEditDto {
     public string? PostId {get; set;}
     public string? Title {get; set;} 
     public string? Content {get; set;} 
     public string? ThumbnailImagePath {get; set;} 
     public IFormFile? ImageFile;
     public DateTime CreationDate {get; set;}
    }
}