using System;
using System.Collections.Generic;

namespace Singleton
{
    public sealed class LoadBalancer
    {
        // Optimization that would remove the use of lock(_locker):
        // this optimization is not used for study/demo purpose.
        // Static members are initialized immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        // private static readonly LoadBalancer instance = new LoadBalancer();

        private static LoadBalancer _instance;
        private static readonly object _locker = new();
        private readonly IList<Server> servers;
        private readonly Random random = new();

        private LoadBalancer()
        {
            servers = new List<Server>
                {
                  new Server{ Name = "ServerI", IP = "120.14.220.18" },
                  new Server{ Name = "ServerII", IP = "120.14.220.19" },
                  new Server{ Name = "ServerIII", IP = "120.14.220.20" },
                  new Server{ Name = "ServerIV", IP = "120.14.220.21" },
                  new Server{ Name = "ServerV", IP = "120.14.220.22" },
                };
        }
        public static LoadBalancer GetLoadBalancer()
        {
            //double checked locking pattern
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadBalancer();
                    }
                }
            }
            return _instance;
        }

        public Server NextServer
        {
            get
            {
                int r = random.Next(servers.Count);
                return servers[r];
            }
        }
    }
}