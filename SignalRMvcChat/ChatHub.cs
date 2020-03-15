using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRMvcChat;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connection = new ConnectionMapping<string>();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            _connection.Add(name,Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            _connection.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            if (!_connection.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connection.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}