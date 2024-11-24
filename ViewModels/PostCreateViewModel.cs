using BlogPost.DTOs;

namespace BlogPost.Models.ViewModels {
    public class PostCreateViewModel{
        public  PostReadDto? Post {get; set;}
        public  List<CheckBox>? Topics {get; set;}
    }
}