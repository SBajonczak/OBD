using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Communicator
{
    public class CommunicatorFactory
    {
        public enum CommunicatorType
        {
            /// <summary>
            /// This is a wifi interface.
            /// </summary>
            Wifi_ELM_327,

            DEVELOP_FAKESERVER
        }


        /// <summary>
        /// Create an 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ICommunicator Create(CommunicatorType type)
        {
            switch (type)
            {
                case CommunicatorType.DEVELOP_FAKESERVER:
                    return new SocketCommunicator("127.0.0.1", 36000);
                case CommunicatorType.Wifi_ELM_327:
                    return new SocketCommunicator("192.168.0.10", 35000);
                default:
                    throw new NotImplementedException("Sorry not implemented yet");
            }
        }
    }
}
