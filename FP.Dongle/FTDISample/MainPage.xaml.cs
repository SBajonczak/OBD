using System.Collections.Generic;
using FTDISample.Helpers;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FTDISample.Hubs;
using FTDISample.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FTDISample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        DeviceConnection obdDevice;
        DeviceConnection devDevice;

        public MainPage()
        {
            InitializeComponent();

            obdDevice = new DeviceConnection();
            devDevice = new DeviceConnection();
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = new System.TimeSpan(0,0,5);
            tmr.Tick += Ticked;
            tmr.Start();
        }


        private async void DoTransmit()
        {
            // Internet available
            IStorage storage = new FileStorage();
            string data = await storage.GetAllData();
            AzureIoTHub.SendEvent(data);
        }


        private async void Ticked(object sender, object e)
        {

            if (await new Ping().DoPing("www.google.de", "80"))
            {
                // Transmit Data
                this.DoTransmit();
            }

            if (await new Ping().DoPing("192.168.0.10", "35000"))
            {
                // Internet not available
                if (obdDevice.IsConnected == false)
                {
                    obdDevice.DoConnect("192.168.0.10");
                }
                if (obdDevice.IsConnected == true)
                    obdDevice.GetAllData();
            }

            if (await new Ping().DoPing("192.168.2.147", "36000"))
            {
                // Internet not available
                if (devDevice.IsConnected == false)
                {
                    devDevice.DoConnect("192.168.2.147");
                }
                if (devDevice.IsConnected == true)
                    devDevice.GetAllData();
            }
            if (await new Ping().DoPing("192.168.2.120", "36000"))
            {
                // Internet not available
                if (devDevice.IsConnected == false)
                {
                    devDevice.DoConnect("192.168.2.147");
                }
                if (devDevice.IsConnected == true)
                    devDevice.GetAllData();
            }
        }
    }
}
