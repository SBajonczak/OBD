using System.ComponentModel;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Communicator
{
    /// <summary>
    /// This interface represents the communication gateway.
    /// </summary>
    public interface ICommunicator: INotifyPropertyChanged
    {

        /// <summary>
        /// Gets. the last received message from the OBD device.
        /// </summary>
        string LastMessage { get; }

        /// <summary>
        /// Indicates wether if the app is connected to the device.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Indicates if the device is ready to retrieve new commands.
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Send the command and receive the results.
        /// </summary>
        Task<string> SendAndReceive(string message);

        /// <summary>
        /// Perform a connect.
        /// </summary>
        Task Connect();

    }
}
