using Microsoft.AspNetCore.SignalR;
using SignalRPractice.Models;
using SignalRPractice.Repository.IRepo;
using System.Text.Json;

namespace SignalRPractice.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatRepository _chatRepo;

        public ChatHub(IChatRepository chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public async Task SendPrivateMessage(string sid, string message, string rid)
        {
            try
            {
                var ChatMessage = new Chat()
                {
                    senderId = sid,
                    recieverId = rid,
                    Message = message,
                    ts = DateTime.Now
                };

                await _chatRepo.AddMessage(ChatMessage);
                await _chatRepo.SaveChangesAsync();

                //await LoadMessage();
                await LoadSepecific(rid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task LoadMessage()
        {
            try
            {
                //var chats = await _chatRepo.GetMessageOfUser(Context.UserIdentifier);
                var chats = await _chatRepo.GetAllMessages();
                await Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(chats));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize("[]"));
            }
        }

        public async Task LoadSepecific(string userId)
        {
            try
            {
                var chats = await _chatRepo.GetMessageOfUser(userId);
                
                await Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(chats));
                //await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", JsonSerializer.Serialize(chats));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
