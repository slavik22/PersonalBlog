namespace DataAccessLayer.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; } = "";

    public ICollection<PostCategory> PostCategories { get; set; }
}