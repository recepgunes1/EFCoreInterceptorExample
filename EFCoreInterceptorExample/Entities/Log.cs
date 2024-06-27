namespace EFCoreInterceptorExample.Entities;

public class Log
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
    public string EntityName { get; set; } = null!;
    public string Operation { get; set; } = null!;
    public Guid EntityId { get; set; }
    public string? BeforeState { get; set; } = null;
    public string? AfterState { get; set; } = null;
}
