using BlogPost.DTOs;
using X.PagedList;

namespace BlogPost.ViewsModels;
public class PostIndexViewModel {
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Topic { get; set; }
    public int? Page {set; get;}
    public string? SortBy {set; get;}
    public IPagedList<PostViewDto>? Items {set; get;}
}