using Microsoft.AspNetCore.Identity;

namespace SignalRPractice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Chat> ChatMesgsSender { get; set; }
        public ICollection<Chat> ChatMesgsReciever { get; set; }

        ApplicationUser()
        {
            ChatMesgsSender = new HashSet<Chat>();
            ChatMesgsReciever = new HashSet<Chat>();
        }
    }
}
