using ClientToServerHandlerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ClientToServerHandlerLib.Models
{
    class Msg : IStringMsg
    {
        public string MainBody { get; set; }
        public string RecvicerNickname { get; set; }
        public string SenderName { get; set; }
        public string SenderIp { get; set; }
        public string RecvicerIp { get; set; }                

    }
}
