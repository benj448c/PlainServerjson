using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace PlainServer
{
    class Server
    {
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 7);
            listener.Start();

            Console.WriteLine("Server started");
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine(  socket.Client.RemoteEndPoint + " connected to the server");
                Task.Run(() =>
                    {
                        TcpClient tempsocket = socket;
                        DoClient(tempsocket);
                    }
                );
            }
        }

        public void DoClient(TcpClient socket)
        {
            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);
                sw.AutoFlush = true;

                while (true)
                {
                    string line = sr.ReadLine();

                    Car car = JsonConvert.DeserializeObject<Car>(line);

                    Console.WriteLine(socket.Client.RemoteEndPoint + " : " + car);

                    sw.WriteLine(car.ToString());
                }

            }
            
        }
    }
}
