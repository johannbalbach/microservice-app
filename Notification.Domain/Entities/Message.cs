using MimeKit;

namespace Notification.Domain.Entities
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
