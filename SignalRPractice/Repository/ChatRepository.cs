using SignalRPractice.Data;
using SignalRPractice.Models;
using SignalRPractice.Repository.IRepo;

namespace SignalRPractice.Repository
{
    public class ChatRepository : IChatRepository
    {

        private readonly Context _context;

        public ChatRepository(Context context)
        {
            _context = context;
        }

        public async Task AddMessage(Chat message)
        {
            await _context.Chats.AddAsync(message);
        }

        public async Task<IEnumerable<Chat>> GetMessageOfUser(string userId)
        {
            return await Task.FromResult(_context.Chats.Where(a => a.senderId == userId || a.recieverId == userId).AsEnumerable());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat?>> GetAllMessages()
        {
            return await Task.FromResult(_context.Chats.AsEnumerable());
        }
    }
}
