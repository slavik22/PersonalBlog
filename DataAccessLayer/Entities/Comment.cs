namespace DataAccessLayer.Entities;

public class Comment : BaseEntity
{
    public string AuthorName { get; set; } = "";

    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public DateTime Published { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }
    
}