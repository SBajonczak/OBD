using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.UI.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        IObdDevice Device;

        public MainPageViewModel()
        {
            this.Device = new Elm();
        }

        private bool _connected;

        private string _IP;
        private int _port;

        public bool Connected { get { return this._connected; } { this._connected = ValueType; OnPropertyChanged("Connected"); } }


        public string IP { get { return _IP; } { this._IP = ValueType; OnPropertyChanged("IP"); } }

        public int Port { get { return _port; } { this._port = ValueType; OnPropertyChanged("Port"); } }


        /// <summary>
        /// 
        /// </summary>
        public void DoConnect()
        {
            ICommunicator communicator = new SocketCommunicator(this.IP, this.Port);
            this.Device.SetCommicatorDevice(communicator);
        }


        

    }
}
