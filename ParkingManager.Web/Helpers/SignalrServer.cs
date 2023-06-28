using Microsoft.AspNetCore.SignalR;

namespace ParkingManager.Web.Helpers
{
    public class SignalrServer : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
