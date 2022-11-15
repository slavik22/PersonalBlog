namespace Data.Entities;

public class Comment : BaseEntity
{
    public string? Title { get; set; }
    public string? Body { get; set; }

    public DateTime Created { get; set; }

    public int? AuthorId { get; set; }
    public int? PostId { get; set; }
    
    public User? Author { get; set; }
    public Post? Post { get; set; }
}