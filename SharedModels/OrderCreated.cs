namespace SharedModels
{
    public record OrderCreated
    {
        public Guid OrderId { get; init; }
        
        public string ProductName { get; init; }
        
        public int Count { get; init; }
    };
}