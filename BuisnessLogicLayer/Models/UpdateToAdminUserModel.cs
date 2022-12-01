using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

public class UpdateToAdminUserModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    
    public string Email { get; set; }= "";
    
    public DateTime BirthDate { get; set; }

    public UserRole UserRole { get; set; }
}