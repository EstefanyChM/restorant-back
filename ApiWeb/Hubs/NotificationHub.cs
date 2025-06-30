using Microsoft.AspNetCore.SignalR;

namespace ApiWeb.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Envía el mensaje a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
