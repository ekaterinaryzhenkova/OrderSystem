namespace NotificationService.Application
{
    public class EmailOptions
    {
        public string Sender { get; set; }
        
        public string AppPassword { get; set; }
        
        public string Subject { get; set; }
        
        public string Host { get; set; }
        
        public int Port { get; set; }
    }
}