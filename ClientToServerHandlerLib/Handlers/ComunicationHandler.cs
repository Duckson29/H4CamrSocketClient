using ClientToServerHandlerLib.Interfaces;
using ClientToServerHandlerLib.Messagse;
using ClientToServerHandlerLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientToServerHandlerLib.Handlers
{
    class ComunicationHandler : IcomHandler
    {
        string IcomHandler.gettingMsg { get { return getmsgRun; } set { getmsgRun = value; } }

        byte[] buffer;
        byte[] bufferSend;
        private TcpClient tcpClient;
        string getmsgRun;
        string messagseToSend;
        string newMsg;
        string ip = "172.16.2.30";
        int port = 0;
        /// <summary>
        /// Empty construtor since the defualt connection is simple string..
        /// </summary>
        public ComunicationHandler()
        {
            buffer = new byte[2048];
            bufferSend = new byte[1024];
            tcpClient = new TcpClient();
        }
        /// <summary>
        /// Use when you need to change the port that this client connects to.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public ComunicationHandler(string ip,int port) : this()
        {
            this.ip = ip;
            this.port = port;            
        }
        /// <summary>
        /// The main connect methoed.
        /// </summary>
        /// <returns></returns>
        bool ConnectToServer()
        {
            if (tcpClient.Connected)
                return true;
            else
            {
                try
                {
                    tcpClient.Connect(IPAddress.Parse(ip), port);
                    Thread recvierThread = new Thread(GetMsg);
                    recvierThread.Start();
                    return true;

                }
                catch (SocketException e)
                {
                    messagseToSend = e.Message;
                    Console.WriteLine(e.Message);
                    return false;
                }
            }

        }
       /// <summary>
       /// This sends a messagse to the connected socket..
       /// </summary>
       /// <param name="messagseToSend"></param>
        void SendMsg(object messagseToSend)
        {          
            Socket s = tcpClient.Client;
            if (messagseToSend is string)
            {
                if (s.Connected)
                {
                    //string t = "Duckson:172.16.2.36:Camilla:172.16.2.30:Beskedher\r\n";
                    bufferSend = Encoding.UTF8.GetBytes(messagseToSend as string);
                    s.Send(bufferSend);
                }
                else
                {
                    Console.WriteLine("Not able to send yet..");
                }
            }
            else if(messagseToSend is Message)
            {
                Message messages = (Message)messagseToSend;
                bufferSend = new XmlMsg().FormatToXmlFromMessgase(messages);
                Console.WriteLine($"trying to send this : {Encoding.UTF8.GetString(bufferSend)}");                
                s.Send(bufferSend);
            }
            else if (messagseToSend is SocketMessage)
            {
                SocketMessage messages = (SocketMessage)messagseToSend;
                bufferSend = new XmlMsg().FormatToXmlFromMessgase(messages);
                List<byte> tempBytes = bufferSend.ToList();
                byte[] endingMsg = Encoding.UTF8.GetBytes("{END}");
                for (int i = 0; i < endingMsg.Length; i++)
                {
                    tempBytes.Add(endingMsg[i]);
                }
                bufferSend = tempBytes.ToArray();
                Console.WriteLine($"trying to send this : {Encoding.UTF8.GetString(bufferSend)}");
                s.Send(bufferSend);
            }
        }
        /// <summary>
        /// This is the network stream reader that is primaly used by a thread started from this class..
        /// </summary>
        void GetStreamMsg()
        {
            byte[] bufferGet = new byte[8000];
            string messgse = "";
            while (tcpClient.Connected)
            {
                Thread.Sleep(1000);
                bufferGet = new byte[tcpClient.Available];
                tcpClient.GetStream().Read(bufferGet, 0, tcpClient.Available);
                messgse = Encoding.UTF8.GetString(bufferGet);
                getmsgRun = messgse;
            }
            getmsgRun = messgse;
        }
        /// <summary>
        /// reads the data from the socket.
        /// </summary>
        public void GetMsg()
        {
            GetStreamMsg();
        }
        /// <summary>
        /// Sends the messagse to the socket.
        /// </summary>
        /// <param name="msgToSend"></param>
        void IcomHandler.SendMsg(object msgToSend)
        {
            if (ConnectToServer())
                SendMsg(msgToSend);
            //Console.WriteLine(SendMsg(msgToSend));
            else
                Console.WriteLine("Nop!!!!!.");
        }
        /// <summary>
        /// Starts the tcpclient and makes the socket ready to send and rescvie messagse.
        /// </summary>
        public void startClient()
        {
            this.ConnectToServer();
        }
    }
}
