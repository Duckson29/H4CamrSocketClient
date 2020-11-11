using ClientToServerHandlerLib;
using ClientToServerHandlerLib.Messagse;
using ClientToServerHandlerLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using ClientToServerHandlerLib.Messagse;
using EncryptionSocketClientH4;

namespace H4CamrSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO:Refactor a lot..
            //TODO:Sepratede the logic from the gui..
            string hostServer = "";
            int hostServerPort = 3;
            List<string> listOfUser = new List<string>();
            string pickUserToSendToo;
            XmlMsg xmlPareser;
            Message msgs;
            Thread xmlThread;

            ClientToServerController clientController;

            Console.WriteLine("Server to connect to\n1.Camillas Server\n2.ImortalServer(Made by Tobias & Wolter)");
            ConsoleKeyInfo serverChose = Console.ReadKey();
            //User picks what server to connect
            switch (serverChose.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    hostServer = "172.16.2.30";
                    hostServerPort = 8888;
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    hostServer = "172.16.2.30";
                    hostServerPort = 50001;
                    break;
            }


            Console.Clear();
            Console.Write("1.SimpleString\n2.Xml\n3.Aes\n4.RSA and Aes.");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    clientController = new ClientToServerController(new ComFactory().CreateComHandler(hostServer, hostServerPort));
                    Thread thread = new Thread(clientController.ComHandler.startClient);
                    thread.Start();
                    string ServerMessagse = "bla";
                    while (key.Key != ConsoleKey.Q)
                    {
                        Console.Clear();
                        ServerMessagse = clientController.ComHandler.gettingMsg;
                        if (ServerMessagse == null)
                            ServerMessagse = "bla bla";
                        if (ServerMessagse.Contains("USER-ONLINE"))
                        {
                            listOfUser = ServerMessagse.Replace("\r\n", "").Replace("USER-ONLINE:", "").Split(":").ToList();
                            for (int i = 0; i < listOfUser.Count; i++)
                            {
                                Console.WriteLine($"{listOfUser[i]}");
                            }
                            Console.WriteLine("What user do you want to send too? : ");
                            ConsoleKeyInfo userInt = Console.ReadKey();
                            Console.Clear();
                            //Console.WriteLine("PickUser" + listOfUser[Convert.ToInt32(Console.ReadLine()) - 1]);
                            //pickUserToSendToo = $"UnkownUser:{listOfUser[Convert.ToInt32(userInt.KeyChar)]}";

                        }
                        else if (ServerMessagse.Contains("172.16.2.36"))
                        {
                            Console.WriteLine($"Messagse : {clientController.ComHandler.gettingMsg.Split(":")[4]}");
                        }
                        Console.WriteLine($"msg reviced{clientController.ComHandler.gettingMsg}");
                        Console.WriteLine("Write msg to send.");
                        string userMsg = $"Duckson:172.16.2.36:Camilla:172.16.2.30:{Console.ReadLine()}";                        
                        clientController.ComHandler.SendMsg((hostServerPort == 50001) ? userMsg += "\r\n" : userMsg = $"Duckson:{userMsg}{{END}}");
                        Console.WriteLine("Press Q to stop \"Chatting\" else press any key.. ");
                        key = Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    xmlPareser = new XmlMsg();
                    msgs = new Message();
                    hostServerPort++;
                    clientController = new ClientToServerController(new ComFactory().CreateComHandler(hostServer, hostServerPort));
                    xmlThread = new Thread(clientController.ComHandler.startClient);
                    xmlThread.Start();
                    while (key.Key != ConsoleKey.Q)
                    {
                        Console.WriteLine(clientController.ComHandler.gettingMsg);
                        Console.WriteLine("User to send too.");
                        msgs.From = new UserInfomation { Name = "Duckson", Ip = "172.16.2.37" };
                        msgs.To = new UserInfomation { Name = "UnkownUser", Ip = Console.ReadLine() };
                        Console.Write("Messagse to send : ");
                        msgs.Mb = new MessagseBody { Body = Console.ReadLine() };
                        if (hostServerPort == 50002)
                        {
                            SocketMessage message = new SocketMessage("Andi", "AndisPC", "172.16.2.37", "Ligon2", "172.16.2.51", "hej hej");
                            clientController.ComHandler.SendMsg(message);
                        }
                        else
                        {
                            clientController.ComHandler.SendMsg(msgs);

                        }
                        Console.WriteLine("Press Q to stop \"Chatting\" else press any key.. ");
                        key = Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    //TODO: Finsihe this with aes.
                    // This is sadly not done yet..
                    xmlPareser = new XmlMsg();
                    msgs = new Message();
                    hostServerPort++;
                    clientController = new ClientToServerController(new ComFactory().CreateComHandler(hostServer, hostServerPort));
                    xmlThread = new Thread(clientController.ComHandler.startClient);
                    xmlThread.Start();
                    string ivkeys = "W+jcxfBJm37AAZujiktg4qCdy3k8D+vIrj4exFxFpIY==";
                    RSAEncryptionController aes = new RSAEncryptionController(new RSAEncrpytion(false));                   
                    while (key.Key != ConsoleKey.Q)
                    {
                        string messagse = xmlPareser.FormatFromXmlToMessgase(Encoding.UTF8.GetBytes(clientController.ComHandler.gettingMsg)).Mb.Body;
                        aes.DecryptDataWithAES(Encoding.UTF8.GetBytes(messagse));
                        Console.WriteLine(aes.DecryptDataWithAES(Convert.FromBase64String(messagse)));
                        Console.WriteLine("User to send too.");
                        msgs.From = new UserInfomation { Name = "Duckson", Ip = "172.16.2.37" };
                        msgs.To = new UserInfomation { Name = "UnkownUser", Ip = Console.ReadLine() };
                        Console.Write("Messagse to send : ");
                        msgs.Mb = new MessagseBody { Body = Console.ReadLine() };
                        if (hostServerPort == 50003)
                        {
                            SocketMessage message = new SocketMessage("Andi", "AndisPC", "172.16.2.37", "Ligon2", "172.16.2.51", "hej hej");
                            clientController.ComHandler.SendMsg(message);
                        }
                        else
                        {
                            clientController.ComHandler.SendMsg(msgs);

                        }
                        Console.WriteLine("Press Q to stop \"Chatting\" else press any key.. ");
                        key = Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    break;
            }
        }

    }
}
