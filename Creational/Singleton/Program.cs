using System;

namespace Singleton
{
    public class Program
    {
        public static void Main()
        {
            CheckBalancers();
           
            var balancer = LoadBalancer.GetLoadBalancer();
            
            for (int i = 0; i < 20; i++)
            {
                var serverName = balancer.NextServer.Name;
                Console.WriteLine($"Request dispatched to: {serverName}");
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void CheckBalancers()
        {
            var b1 = LoadBalancer.GetLoadBalancer();
            var b2 = LoadBalancer.GetLoadBalancer();
            var b3 = LoadBalancer.GetLoadBalancer();
            var b4 = LoadBalancer.GetLoadBalancer();
            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                Console.WriteLine("Balancers are same instance.");
            }
        }
    }
}