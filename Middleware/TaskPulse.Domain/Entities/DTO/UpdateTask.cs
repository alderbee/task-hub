using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Domain.Entities.DTO;

public class UpdateTask
{
    public int TaskId { get; set; }  
    
    public int UserId { get; set; }  
  
    public string Title { get; set; } 
    
    public byte Status { get; set; } 

    public DateTime? StartDate { get; set; }  

    public DateTime? EndDate { get; set; }

    public string? Content { get; set; }  
    
    public int? PriorityId { get; set; }  
}