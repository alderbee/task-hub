using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities.DTO;

public class UserRegistration
{
    public string? username { get; set; }
    public string? email { get; set; }

    public string? password { get; set; }
    
    public string? captchToken { get; set; }

    public bool premium { get; set; } = true;

    public bool active { get; set; } = true;

}