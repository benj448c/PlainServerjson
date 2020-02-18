using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using ModelLib;
using Newtonsoft.Json;

namespace PlainClient
{
    class Client
    {
        public void Start()
        {
            TcpClient client = new TcpClient("localhost", 7);
            using (client)
            {
                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);
                sw.AutoFlush = true;

                while (true)
                {

                    Car car = new Car();

                    Console.Write("Indtast bilens farve: ");
                    car.Color = Console.ReadLine();
                    Console.Write("Indtast bilens model: ");
                    car.Model = Console.ReadLine();
                    Console.Write("Indtast bilens registeringsnummer: ");
                    car.RegistrationNumber = Int32.Parse(Console.ReadLine());

                    string serializedcar = JsonConvert.SerializeObject(car);

                    sw.WriteLine(serializedcar);

                    Console.WriteLine(sr.ReadLine());
                    Console.ReadKey();
                }

            }
        }

    }
}
