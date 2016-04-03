﻿using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Helpers;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{

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
        /// When command was received
        /// </summary>
        event AfterResultReceivedDelegate AfterResultReceived;

        /// <summary>
        /// Sending the command.
        /// </summary>
        /// <param name="command"></param>
        Task<ElmDecoder> SendAndReceive(string command);

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