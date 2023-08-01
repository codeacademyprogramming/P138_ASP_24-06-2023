using Microsoft.AspNetCore.SignalR;

namespace P138SignalR2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task MesajGonder(string name, string msg)
        {
            await Clients.All.SendAsync("MesajQebulEden",name, msg);
        }
    }
}
