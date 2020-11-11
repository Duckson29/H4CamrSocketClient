using ClientToServerHandlerLib.Messagse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace ClientToServerHandlerLib.Models
{
    public class XmlMsg
    {
        /// <summary>
        /// This for testing to Camillas server program..
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public byte[] FormatToXml(string msg)
        {
            return Encoding.UTF8.GetBytes($"<?xml version = '1.0' ?>" +
"<Message xmlns:xsi = 'http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd = 'http://www.w3.org/2001/XMLSchema'>" +
  "<To>" +
    "<Name>Kenthe</Name>" +
    "<Ip>172.16.2.38</Ip>" +
  "</To>" +
  "<From>" +
    "<Name>Andi</Name>" +
    "<Ip>172.16.2.36</Ip>" +
  "</From>" +
  "<Mb>" +
    "<Body>blabla</Body>" +
  "</Mb>" +
  "<Users/>" +
"</Message>");
        }

        public byte[] FormatToXmlFromMessgase(object msg) 
        {
            if (msg is Message)
            {

            XmlSerializer ser = new XmlSerializer(typeof(Message));
            byte[] bufferStream = new byte[1024];
            ser.Serialize(new MemoryStream(bufferStream), msg);
            return bufferStream;
            }
            else if(msg is SocketMessage)
            {
                XmlSerializer ser = new XmlSerializer(typeof(SocketMessage));
                byte[] bufferStream = new byte[1024];
                ser.Serialize(new MemoryStream(bufferStream), msg);
                return bufferStream;
            }
            return null;
        }
        public Message FormatFromXmlToMessgase(byte[] msg) 
        {
            XmlSerializer ser = new XmlSerializer(typeof(Message));
            byte[] bufferStream = msg;
            Message resultMessage = (Message)ser.Deserialize(new MemoryStream(bufferStream));
            return resultMessage;


        }
    }
}
