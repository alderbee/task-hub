using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities;

public class UsersData
{
    [Key] [Required] public int userId { get; set; }

    [Required] [MaxLength(8)] public string? username { get; set; }

    [Required] [EmailAddress] public string? email { get; set; }

    [Required] public string? passwordHash { get; set; }

    public bool premium { get; set; }
    
    public bool active { get; set; }

    public DateTime createdDate { get; set; }

    public DateTime updateDate { get; set; }
}