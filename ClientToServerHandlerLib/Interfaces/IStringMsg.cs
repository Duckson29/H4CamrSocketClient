using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ClientToServerHandlerLib.Interfaces
{
    public interface IStringMsg
    {
        string MainBody { get; set; }
        string RecvicerNickname { get; set; }
        string SenderName { get; set; }
        string SenderIp { get; set; }
        string RecvicerIp { get; set; }
    }
}
