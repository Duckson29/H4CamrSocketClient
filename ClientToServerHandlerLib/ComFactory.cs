using ClientToServerHandlerLib.Handlers;
using ClientToServerHandlerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerHandlerLib
{
    public class ComFactory
    {
        public IcomHandler CreateComHandler()
        {
            return new ComunicationHandler();
        }
        public IcomHandler CreateComHandler(string hostname,int hostport)
        {
            return new ComunicationHandler(hostname,hostport);
        }
    }
}
