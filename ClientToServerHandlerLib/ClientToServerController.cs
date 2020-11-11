using ClientToServerHandlerLib.Handlers;
using ClientToServerHandlerLib.Interfaces;
using ClientToServerHandlerLib.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientToServerHandlerLib
{
    public class ClientToServerController
    {
        
        public IcomHandler ComHandler { get; set; }
        /// <summary>
        /// Defualt IComHandler
        /// </summary>
        /// <param name="icomHandler"></param>
        public ClientToServerController(IcomHandler icomHandler)
        {          
            ComHandler = (ComHandler == null) ?  icomHandler : new ComunicationHandler();
        }                        
    }
}
