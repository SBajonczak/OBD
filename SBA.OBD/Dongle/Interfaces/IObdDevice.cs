using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Decoders;
using SBA.OBD.Dongle.Helpers;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{
    /// <summary>
    /// Represents the ODB device type.
    /// </summary>
    /// <remarkt>This is a good way to differ between ELM or an CANBUS.</example>
    public interface IObdDevice: INotifyPropertyChanged
    {

        /// <summary>
        /// The Data Channel
        /// </summary>
        ICommunicator CommunicatorDevice { get; }

        /// <summary>
        /// Set the <see cref="ICommunicator"/>.
        /// </summary>
        /// <param name="communicator"></param>
        void SetCommicatorDevice(ICommunicator communicator);

    
        /// <summary>
        /// Sending the command.
        /// </summary>
        /// <param name="command"></param>
        Task<IDecoder> SendAndReceive(string command);

        /// <summary>
        /// Get the results
        /// </summary>
        void Receive();

        /// <summary>
        /// Perform a Connection
        /// </summary>
        Task Connect();

        /// <summary>
        /// Perform a disconnet.
        /// </summary>
        void Disconnect();


        bool IsConnected { get; }

        /// <summary>
        /// Is the Devices Ready
        /// </summary>
        /// <returns></returns>
        bool IsReady {get; }

        /// <summary>
        /// The last Errormessage.
        /// </summary>
        string LastMessage{ get; }

        /// <summary>
        /// Get the device Name
        /// </summary>
        /// <returns></returns>
        string DeviceName {get; }

        /// <summary>
        /// Resetting the device
        /// </summary>
        void ResetDevice();

        /// <summary>
        /// Settting the protocol.
        /// </summary>
        /// <param name="protocol"></param>
        void SetProtocoll(Helpers.Enumerations.Protocol protocol);

     
    }
}