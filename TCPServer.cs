using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server
{

    class Class1
    {

        static void Main(String[] args)
        {

           WaitForConnections();

    }
        static void WaitForConnections()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = IPAddress.Parse("<IP here>");
            int port = 11111; 
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

            Console.WriteLine(localEndPoint.ToString());

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {


                while (true)
                {
                    listener.Bind(localEndPoint);
                    listener.Listen();

                    Console.WriteLine("waiting for response");
                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new Byte[1024];
                    string data = null;
                    Console.WriteLine(data);

                    while (true)
                    {
                        int numByte = clientSocket.Receive(bytes);

                        data = Encoding.ASCII.GetString(bytes, 0, numByte);


                        Console.WriteLine("message from user: " + data);

                        byte[] message = Encoding.ASCII.GetBytes("message from server: " + data);
                        clientSocket.Send(message);
                    }

                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}