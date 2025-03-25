namespace ServiceDefaults.Messaging.Events;

public record ProductPriceChangedIntegrationEvent: IntegrationEvent
{
    public int ProductId { get; set; }
    
    public string? Name {get; set;} 
    
    public string? Description {get; set;}
    
    public decimal Price {get; set;}
    
    public string? ImageUrl {get; set;}
    
}