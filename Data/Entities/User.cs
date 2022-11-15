using Data.Enums;

namespace Data.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public string? Email { get; set; }
    public string? Password { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public UserRole UserRole { get; set; }
    
    //public byte[] Image { get; set; }

    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}