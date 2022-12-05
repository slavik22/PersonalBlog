using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Content { get; set; } = "";

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PostStatus PostStatus { get; set; } 
    
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
    public ICollection<PostTag> PostTags  { get; set; }
    public ICollection<PostCategory> PostCategories { get; set; }

}