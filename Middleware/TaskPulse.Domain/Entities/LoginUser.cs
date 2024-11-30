using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities;

public record LoginUser()
{
    [Required]
    public string? username { get; set; }
    [Required]
    public string? password { get; set; }
    
    [Required]
    public string? captchToken { get; set; }
    
}