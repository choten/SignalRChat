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
            if (string.IsNullOrEmpty(name))
            {
                string caller = Context.QueryString["name"];
                Clients.All.addNewMessageToPage(caller, message);
            }
            else
            {
                string connectId = _connection.GetConnections(name).FirstOrDefault();
                string caller = Context.QueryString["name"];
                Clients.Client(connectId).addNewMessageToPage(caller, message);
            }
        }

        public override Task OnConnected()
        {
            //string name = Context.User.Identity.Name;
            string name = Context.QueryString["name"];
            _connection.Add(name,Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //string name = Context.User.Identity.Name;
            string name = Context.QueryString["name"];
            _connection.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            //string name = Context.User.Identity.Name;
            string name = Context.QueryString["name"];
            if (!_connection.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connection.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}