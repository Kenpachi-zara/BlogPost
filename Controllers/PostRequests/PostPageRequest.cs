namespace BlogPost.PostRequests;
public class PostPageRequest : PageRequest{

    public PostPageRequest()
    {}
    
    public PostPageRequest(string? title, string? author, string? topic, int? page, string? sortBy, bool nextPage) : base(page, sortBy)
    {
        Title = title;
        Author = author;
        Topic = topic;
        Page = page;
        SortBy = sortBy;
        NextPage = nextPage;
    }

    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Topic { get; set; }
    public bool NextPage { get; set; }
}