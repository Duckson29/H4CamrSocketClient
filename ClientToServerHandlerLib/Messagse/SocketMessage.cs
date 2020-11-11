using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerHandlerLib.Messagse
{
    public class SocketMessage
    {
        //ImortalServer(Tobais and wolter)
        // xml Format..
            //<?xml version="1.0" encoding="utf-16"?>
            //<SocketMessage xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
            //<NickName>BabyChris</NickName>
            //<SenderHostName>BabyChrisPC</SenderHostName>
            //<SenderIpAddress>127.0.0.1</SenderIpAddress>
            //<ReceiverHostName>TitanChrisPC</ReceiverHostName>
            //<ReceiverIpAddress>127.0.0.1</ReceiverIpAddress>
            //<ChatMessage>Colosal Chris</ChatMessage>
            //</SocketMessage>{END}      
            
        string nickName;
        string senderHostName;
        string senderIpAddress;
        string receiverHostName;
        string receiverIpAddress;
        string chatMessage;

        public string NickName { get => nickName; set => nickName = value; }
        public string SenderHostName { get => senderHostName; set => senderHostName = value; }
        public string SenderIpAddress { get => senderIpAddress; set => senderIpAddress = value; }
        public string ReceiverHostName { get => receiverHostName; set => receiverHostName = value; }
        public string ReceiverIpAddress { get => receiverIpAddress; set => receiverIpAddress = value; }
        public string ChatMessage { get => chatMessage; set => chatMessage = value; }
        public SocketMessage()
        {

        }
        public SocketMessage(string _nickName, string _senderHostName, string _senderIpAddress, string _receiverHostName, string _receiverIpAddress, string _chatMessage) : base()
        {
            NickName = _nickName;
            SenderHostName = _senderHostName;
            SenderIpAddress = _senderIpAddress;
            ReceiverHostName = _receiverHostName;
            ReceiverIpAddress = _receiverIpAddress;
            ChatMessage = _chatMessage;
        }

    }
}
