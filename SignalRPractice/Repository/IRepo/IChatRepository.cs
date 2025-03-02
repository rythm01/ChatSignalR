using SignalRPractice.Models;

namespace SignalRPractice.Repository.IRepo
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetMessageOfUser(string userId);
        Task<IEnumerable<Chat?>> GetAllMessages();
        Task AddMessage(Chat message);
        Task SaveChangesAsync();
    }
}
