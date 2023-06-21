// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDP_Socket_Programming_Fonksiyonları;

class Program
{
    private const int BufferSize = 1024;

    static void Main(string[] args)
    {
        Console.WriteLine("Enter 's' for server 'c' for client");

        string input = Console.ReadLine();

        if (input.Equals("s"))
        {
            Console.WriteLine("Ip Number: 127.0.0.1");
            Console.WriteLine("Starting server Port Number: 50000");

            UDPSocket server = new UDPSocket("127.0.0.1", 50000);
            string receivedMessage = server.Echo();

            Console.WriteLine($"Server received {receivedMessage}");
        }
        else if (input.Equals("c"))
        {
            Console.WriteLine("Ip Number: 127.0.0.1");
            Console.WriteLine("Starting client Port Number: 60000");
            Console.WriteLine("birşeyler yazın");
            string a = Console.ReadLine();

            UDPSocket client = new UDPSocket("127.0.0.1", 60000);
            client.Send(a, "127.0.0.1", 50000);
            string receivedMessage = client.Listen();

            Console.WriteLine($"Client received {receivedMessage}");
        }
        else
        {
            Console.WriteLine("Unexpected input!");
        }
        Console.WriteLine("Press any key to quit!");
        Console.ReadLine();
    }
}
