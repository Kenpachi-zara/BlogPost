using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Models;
public class Post{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public Guid PostId {get; set;}

     [Required]
     public string? Title {get; set;} 

     [Required]
     public string? Content {get; set;}

     [Required]
     public string? ThumbnailImagePath {get; set;}

     [Required]
     public string? Author {get; set;} //related to UserName -> AspNetUsers table. 

     [Required]
     public DateTime CreationDate {get; set;} 
     public DateTime? ModifiedDate {get; set;}     
     public ICollection<TopicsPosts>? TopicsPosts {get; set;}
 }