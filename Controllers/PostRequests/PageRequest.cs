namespace BlogPost.PostRequests;
public class PageRequest{
    public PageRequest()
    {  }

    public PageRequest(int? page, string? sortBy)
    {
        Page = page;
        SortBy = sortBy;
    }

    public int? Page {set; get;}
    public string? SortBy {set; get;}
}