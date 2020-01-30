using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace XServer.XSockets
{
    class XListener
    {
        Socket m_ListenerSocket;
        int m_Port;

        public XListener(int nPort)
        {
            this.m_Port = nPort;
            m_ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void StartListen()
        {
            m_ListenerSocket.Bind(new IPEndPoint(IPAddress.Any, this.m_Port));
            m_ListenerSocket.Listen(10);

            m_ListenerSocket.BeginAccept(new AsyncCallback(OnAccept), null);

        }
        void OnAccept(IAsyncResult ar) 
        {
            Socket temp=m_ListenerSocket.EndAccept(ar);

        }
    }
}
