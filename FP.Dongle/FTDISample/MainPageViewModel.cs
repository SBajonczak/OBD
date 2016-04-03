using System;
using System.ComponentModel;
using System.Windows.Input;
using FTDISample.Helpers;
using UartSample.Devices;
using FTDISample.Devices.Enumerations;
using FTDISample.Devices;
using System.Collections.Generic;
using System.Text;
using FTDISample.Storage;

namespace FTDISample
{
    public class DeviceConnection : BaseViewModel, INotifyPropertyChanged
    {
        private bool isConnected;
        private string messageLog;
        IStorage storage;


        IObdDevice device;
        
        public bool IsConnected
        {
            get { return isConnected; }
            private set { isConnected = value; OnPropertyChanged(); }
        }



        /// <summary>
        /// Get all the data from the OBD INterface
        /// </summary>
        public async void GetAllData()
        {
            //storage.Init();

            
            await device.SendAndReceive("ATZ");
        
            ElmDecoder decoder= null;
            if (device.IsConnected)
                decoder = await device.SendAndReceive(Devices.Enumerations.ElmCommands.GetODOmeter);

        
            if (decoder != null)
            {
                int i;
                IInterfaceData interfaceData = new InterfaceData("OBD-Device-1");

                if (int.TryParse(decoder.DecodedValue, out i))
                    interfaceData.ODOMeter = i;

                this.IsConnected = device.IsConnected;

                OnPropertyChanged("IsConnected");
                if (!decoder.HasError)
                {
                    OnPropertyChanged("MessageLog");
                    storage.WriteData(
                       string.Format("stamp:{3};DeviceID:{2};ODO:{0};ERR:{1}",
                       interfaceData.ODOMeter,
                       interfaceData.ErrorCode,
                       interfaceData.DeviceID,
                       interfaceData.Stamp)
                       );
                    await Hubs.AzureIoTHub.SendEvent(interfaceData);
                }
                else
                {
                    this.messageLog += "Result was incorrect, so it would not be send";
                    OnPropertyChanged("MessageLog");

                }
            }
        }


        private bool isConnecting;

        public async void DoConnect(object ip)
        {
            if (isConnecting)
                return;

            this.IsConnected = false;
            try
            {
                System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
                isConnecting = true;
                device = new ElmWifi(ip.ToString(), 36000);
                await this.device.Connect();
                this.IsConnected =this.device.IsConnected;

                this.IsConnected = this.device.IsConnected;

                isConnecting = false;
                OnPropertyChanged("IsConnected");
            }
            catch (Exception e)
            {
            }
        }

        private void NetworkChange_NetworkAddressChanged(object sender, System.EventArgs e)
        {

        }

        public DeviceConnection()
        {
            this.storage = new FileStorage();

            device = new ElmWifi("192.168.0.10", 35000);
            //devdevice = new ElmWifi("192.168.2.147", 36000);
            device.PropertyChanged += PropertyChanged;
            
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.IsConnected = ((ElmWifi)device).IsConnected ;
        }

    }

}
