using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XServer.XMessages
{
    public enum XMessagesProtocols : byte
    {
        HEADER = 254,
        CHAT_EVENT =1,
        INFO_EVENT =2,
    }
}
