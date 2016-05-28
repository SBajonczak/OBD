using Microsoft.ApplicationInsights;
using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Decoders;
using SBA.OBD.Dongle.Helpers;
using SBA.OBD.Dongle.Helpers.Enumerations;
using System;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{

    /// <summary>
    /// An ELM interface.
    /// </summary>
    public class Elm:BaseObject, IObdDevice
    {
        TelemetryClient TelemetryClient;

        /// <summary>
        /// Ist aktuell verbunden?
        /// </summary>
        public bool IsConnected
        {
            get {
                return this.CommunicatorDevice.IsConnected;
            }


        }

        public bool IsReady
        {
            get
            {
                return this.CommunicatorDevice.IsReady;
            }
        }

        public string LastMessage
        {
            get
            {
                return this.CommunicatorDevice.LastMessage;
            }
        }

        public string DeviceName
        {
            get; private set;
        }

        public ICommunicator CommunicatorDevice
        {
            get; set;
        }

        public Elm()
        {
            this.TelemetryClient = new TelemetryClient();
        }


        public async Task<IDecoder> SendAndReceive(string command)
        {
            try
            {
                
                string result = await this.CommunicatorDevice.SendAndReceive(command);
                return new ElmDecoder(result);
            }
            catch (Exception ex)
            {
                TelemetryClient.TrackException(ex);
            }
            finally
            {
                this.OnPropertyChanged("IsConnected");
            }
            return new ElmDecoder("");
        }

        public async void ResetDevice()
        {
            await SendAndReceive("ATZ");
        }


        public async void SetProtocoll(Protocol protocol)
        {
            await SendAndReceive("SP" + protocol.ToString());
        }

        public void Disconnect()
        {
            // Disconnect was not used here.
        }

        public void Receive()
        {
        }



        public async Task Connect()
        {
            try
            {
                await this.CommunicatorDevice.Connect();
                //Set state to connected
                OnPropertyChanged("IsConnected");
            }
            catch (Exception ex)
            {
                TelemetryClient.TrackException(ex);
            }
        }

        public async void SetCommicatorDevice(ICommunicator communicator)
        {
            if (this.CommunicatorDevice != null)
                this.Disconnect();
            this.CommunicatorDevice = communicator;

            await this.Connect();
        }
    }
}
