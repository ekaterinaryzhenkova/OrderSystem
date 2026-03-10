namespace SharedModels
{
    public record StoreResponse
    {
        public Guid OrderId { get; init; }

        public bool IsSuccess { get; init; }
        
        public string Message { get; init; }
    }
}