namespace SharedModels
{
    public record OrderNotification
    {
        public Guid OrderId { get; init; }
        
        public string Message { get; init; }
        
        public string Email { get; init; }
    }
}