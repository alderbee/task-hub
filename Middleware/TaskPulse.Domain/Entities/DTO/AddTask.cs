using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities.DTO;

public class AddTask
{
    public int? UserId { get; set; }  
    
    public string? Title { get; set; } 
    
    public byte? Status { get; set; } 

    public DateTime? StartDate { get; set; }  

    public DateTime? EndDate { get; set; }
    
    public string? Content { get; set; }  
    
    public byte? PriorityId { get; set; }  
}