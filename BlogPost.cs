public class BlogPost : IIdentifiable
{
    public int Id { get; set; }
    public string Title { get; set; }

    public BlogPost(int id, string title)
    {
        Id = id;
        Title = title;
    }
}
