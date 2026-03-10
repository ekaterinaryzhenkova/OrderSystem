namespace SharedModels
{
    public record OrderCreated
    {
        public Guid OrderId { get; init; }
        
        public Guid ProductId { get; init; }
        
        public int Count { get; init; }
    };
}