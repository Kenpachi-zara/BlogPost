using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Models{

    [Index(nameof(Name), IsUnique = true)]
   public class Topic{

      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public Guid TopicId {get; set;}

      [Required]
      public string? Name {get; set;}
      
      public ICollection<TopicsPosts>? TopicsPosts {get; set;}
   }
}