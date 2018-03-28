using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace NetInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            IPGlobalProperties propertiesIP = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("Host name: " + propertiesIP.HostName);
            Console.WriteLine("Domain name: " + propertiesIP.DomainName);
            Console.WriteLine();

            int i = 0;
            foreach (NetworkInterface netCards in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Card #" + ++i + ": " + netCards.Id);
                Console.WriteLine("MAC Address: " + netCards.GetPhysicalAddress().ToString());
                Console.WriteLine("Name: " + netCards.Name);
                Console.WriteLine("Description :" + netCards.Description);
                Console.WriteLine("Status : " + netCards.OperationalStatus);
                Console.WriteLine("Speed : " + (netCards.Speed) / (double)1000000 + "Mb/s");
                Console.WriteLine("Gateway addresses : ");
                foreach (GatewayIPAddressInformation gatewayAddress in netCards.GetIPProperties().GatewayAddresses)
                    Console.WriteLine("    " + gatewayAddress.Address.ToString());
                Console.WriteLine("DNS servers:");
                foreach (IPAddress addressIP in netCards.GetIPProperties().DnsAddresses)
                    Console.WriteLine("    " + addressIP.ToString());
                Console.WriteLine("DHCP servers: ");
                foreach (IPAddress addressIP in netCards.GetIPProperties().DhcpServerAddresses)
                    Console.WriteLine("    " + addressIP.ToString());
                Console.WriteLine("WINS servers: ");
                foreach (IPAddress addressIP in netCards.GetIPProperties().WinsServersAddresses)
                    Console.WriteLine("    " + addressIP.ToString());
                Console.WriteLine("");
            }

            Console.WriteLine("Current TCP/IP connections type: client :" );
            foreach (TcpConnectionInformation tcpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections())
            {
                Console.WriteLine("    Remote address :" + tcpConnection.RemoteEndPoint.Address.ToString() + ": " + tcpConnection.RemoteEndPoint.Port);
                Console.WriteLine("    Status: " + tcpConnection.State.ToString());
            }

            Console.WriteLine("Current TCP/IP connections type: server :");
            foreach (IPEndPoint tcpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners())
                Console.WriteLine("    Remote address :" + tcpConnection.Address.ToString() + ": " + tcpConnection.Port);
            Console.WriteLine("Current UDP connections: ");
            foreach (IPEndPoint udpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners())
                Console.WriteLine("    Remote address: " + udpConnection.Address.ToString() + ": " + udpConnection.Port);
            Console.ReadLine();
        }
    }
}
