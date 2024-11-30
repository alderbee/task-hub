namespace TaskPulse.Domain.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";
    
    public string DatabaseConnection { get; set; } = null!;
}