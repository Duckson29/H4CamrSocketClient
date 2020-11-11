using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerHandlerLib.Messagse
{
    public class Message
    {
        public UserInfomation From { get; set; }
        public UserInfomation To { get; set; }
        public MessagseBody Mb { get; set; }
    }
}
