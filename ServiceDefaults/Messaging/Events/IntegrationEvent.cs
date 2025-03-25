namespace ServiceDefaults.Messaging.Events;


//TODO: For simplicity we are using here ServiceDefaults we must move this into shared library
public record IntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    
    public DateTime OccurredOn => DateTime.UtcNow;

    public string? EventType => GetType().AssemblyQualifiedName;
}