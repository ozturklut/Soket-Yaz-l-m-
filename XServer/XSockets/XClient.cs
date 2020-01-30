using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XServer.XMessages;

namespace XServer.XSockets
{
    public delegate void OnMessageReceived(ChatCommand cc);
    class XClient
    {
        Socket m_ClientSocket;
        byte[] m_Buffer = new byte[1024];

        public OnMessageReceived m_OnMessageReceived;

        public XClient(Socket nClientSocket) 
        {
            this.m_ClientSocket = nClientSocket;
        }

        public void StartRelay()
        {
            m_ClientSocket.BeginReceive(m_Buffer ,0,m_Buffer.Length,SocketFlags.None,new AsyncCallback(OnReceive),null);


        }
        void OnReceive(IAsyncResult ar)
        {
            int length = m_ClientSocket.EndReceive(ar);
            if (length <= 0)
            {
                return;
            }
            ExtractBuffer(m_Buffer, length);
        }

        private void ExtractBuffer(byte[] m_Buffer, int length)
        {
            byte[] SizedBuffer = new byte[length];
            Array.Copy(m_Buffer, 0, SizedBuffer, 0, SizedBuffer.Length);

            if (SizedBuffer[0] == (byte)XMessagesProtocols.HEADER)
            {
                XMessagesProtocols xmp = (XMessagesProtocols)SizedBuffer[1];

                switch (xmp)
                {
                    
                    case XMessagesProtocols.CHAT_EVENT:
                        if (m_OnMessageReceived != null)
                            m_OnMessageReceived(ChatCommand.Fr);
                        break;
                    case XMessagesProtocols.INFO_EVENT:
                        break;
                    default:
                        break;
                }
            }
            else {
                Console.WriteLine("Invalid buffer recevid..");
            }
        }
        byte[] GetCommand(byte[] data) {
            byte[] temp = new byte[data.Length - 2];
            Array.Copy(data, 2, temp, 0, temp.Length);
            return temp;
        }
    }
}
