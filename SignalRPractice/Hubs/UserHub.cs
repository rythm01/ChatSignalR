using Microsoft.AspNetCore.SignalR;

namespace SignalRPractice.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalView { get; set; } = 0;

        public static int ViewCount { get; set; } = 0;
        public override Task OnConnectedAsync()
        {
            ViewCount++;
            Clients.All.SendAsync("updateConn", ViewCount).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ViewCount--;
            Clients.All.SendAsync("updateConn", ViewCount).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Increament()
        {
            TotalView++;
            await Clients.All.SendAsync("updateCount", TotalView);
        }
    }
}
