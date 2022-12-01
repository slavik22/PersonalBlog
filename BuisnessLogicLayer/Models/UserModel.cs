using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

public class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    
    public string Email { get; set; }= "";
    public string Password { get; set; }= "";
    
    public string Token { get; set; }= "";
    
    public DateTime BirthDate { get; set; }

    public UserRole UserRole { get; set; }
}