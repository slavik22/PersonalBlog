using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

public class PostToAddModel
{
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Content { get; set; } = "";

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string AuthorName { get; set; } = "";
    public int UserId { get; set; }

    public ICollection<CategoryModel> CategoryModels { get; set; }
}