using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities;

public class TaskModel
{
    [Key] 
    [Required] 
    public int? TaskId { get; set; }  

    [Required] 
    public int? UserId { get; set; }  

    [Required] 
    [MaxLength(25)] 
    public string? Title { get; set; } 

    [Required] 
    public byte? Status { get; set; } 

    public DateTime? StartDate { get; set; }  

    public DateTime? EndDate { get; set; }

    [Required] 
    public DateTime? CreatedAt { get; set; } = DateTime.Now; 

    public DateTime? UpdatedAt { get; set; } 

    public string? Content { get; set; }  
    
    public byte? PriorityId { get; set; }  
    
    
}