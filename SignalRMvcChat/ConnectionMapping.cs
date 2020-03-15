using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMvcChat
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get 
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connection;
                if (!_connections.TryGetValue(key,out connection))
                {
                    connection = new HashSet<string>();
                    _connections.Add(key, connection);
                }

                lock (connection)
                {
                    connection.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connection;
            if (_connections.TryGetValue(key, out connection))
            {
                return connection;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            HashSet<string> connection;
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out connection))
                {
                    return;
                }

                lock (connection)
                {
                    connection.Remove(connectionId);

                    if (connection.Count == 0)
                    {
                        _connections.Remove(key); 
                    }
                }
            }
        }
    }
}