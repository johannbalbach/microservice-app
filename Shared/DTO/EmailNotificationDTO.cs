using MimeKit;

namespace Shared.DTO
{
    public class EmailNotificationDTO
    {
        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
