namespace DataAccessLayer.Entities;

public class Tag : BaseEntity
{
    public string Title { get; set; } = "";
    public IEnumerable<PostTag> PostTags { get; set; }
}