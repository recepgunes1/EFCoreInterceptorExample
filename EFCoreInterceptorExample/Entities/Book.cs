namespace EFCoreInterceptorExample.Entities;

public class Book : ILoggable
{
    public  Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Pages { get; set; }
}
