using BlogPost.DTOs;

namespace BlogPost.ViewsModels;

public class PostEditViewModel{
        public PostEditDto? Post {get; set;}
        public List<CheckBox>? Topics {get; set;}
}