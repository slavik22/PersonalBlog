namespace DataAccessLayer.Entities;

public class Tag : BaseEntity
{
    public string Title { get; set; } = "";
    public ICollection<PostTag> PostTags { get; set; }
}