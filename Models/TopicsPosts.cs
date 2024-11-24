using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Models
{
    public class TopicsPosts {
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Key]
      public Guid TopicsPostsId {get; set;}

      [Required]
      public Guid PostId {get; set;}

      [Required]
      public Guid TopicId {get; set;}
      
      public Topic? Topic {get; set;}
      public Post? Post {get; set;}
    }
}