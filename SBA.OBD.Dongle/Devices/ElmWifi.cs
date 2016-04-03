using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Helpers;
using SBA.OBD.Dongle.Helpers.Enumerations;
using System;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{
    public class Elm:BaseObject, IObdDevice
    {

        /// <summary>
        /// Letztes Ergebnis.
        /// </summary>
        private string Lastresponse;

        public event AfterResultReceivedDelegate AfterResultReceived;

        /// <summary>
        /// Ist aktuell verbunden?
        /// </summary>
        public bool IsConnected
        {
            get; private set;
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
        }


        public async Task<ElmDecoder> SendAndReceive(string command)
        {
            try
            {
                
                string result = await this.CommunicatorDevice.SendAndReceive(command);
                return new ElmDecoder(result);
            }
            catch (Exception)
            {
            }
            finally
            {
                this.IsConnected = this.CommunicatorDevice.IsConnected;
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
            this.IsConnected = false;
            try
            {
                await this.CommunicatorDevice.Connect();
                //Set state to connected
                this.IsConnected = this.CommunicatorDevice.IsConnected;
                OnPropertyChanged("IsConnected");
            }
            catch (Exception)
            {
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
