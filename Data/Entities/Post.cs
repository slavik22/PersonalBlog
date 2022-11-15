using Data.Enums;

namespace Data.Entities;

public class Post : BaseEntity
{
    public string? Title { get; set; }
    public string? Body { get; set; }

    public DateTime Created { get; set; }
    public PostStatus PostStatus { get; set; } 
    //public byte[] Image { get; set; }
    
    public int? AuthorId { get; set; }
    
    public User? Author { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}