namespace SignalRPractice.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string senderId { get; set; }
        public string recieverId { get; set; }
        public string Message { get; set; }
        public DateTime ts { get; set; }

        public ApplicationUser sender { get; set; }
        public ApplicationUser reciever { get; set; }
    }
}
