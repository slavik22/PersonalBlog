using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class PostCategory
{
    [Key,Column(Order = 0)]
    public int PostId { get; set; }
    [Key,Column(Order = 1)]
    public int CategoryId { get; set; }
    
    public Post Post { get; set; }
    public Category Category { get; set; }
}
