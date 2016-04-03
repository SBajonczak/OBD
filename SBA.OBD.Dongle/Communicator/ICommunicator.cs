using System.ComponentModel;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Communicator
{
    public interface ICommunicator: INotifyPropertyChanged
    {


        string LastMessage { get; }
        bool IsConnected { get; }
        bool IsReady { get; }

        /// <summary>
        /// Perofrm A send and Receive
        /// </summary>
        Task<string> SendAndReceive(string message);

        /// <summary>
        /// Perform a connect.
        /// </summary>
        Task Connect();

    }
}
