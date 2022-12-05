using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public UserRole UserRole { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}