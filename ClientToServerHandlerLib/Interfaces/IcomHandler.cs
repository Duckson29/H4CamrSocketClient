using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerHandlerLib.Interfaces
{
    public interface IcomHandler
    {
        void GetMsg();
        void SendMsg(object msgToSend);
        string gettingMsg { get; set; }
        void startClient();
    }
}
