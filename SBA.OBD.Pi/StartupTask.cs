using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using SBA.OBD.Dongle.Devices;
using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Helpers;
using SBA.OBD.Dongle.Helpers.Enumerations;
using SBA.ODB.Storage.Storage;
using SBA.ODB.Storage.Hubs;
using System.Diagnostics.Tracing;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SBA.OBD.Pi
{
    public sealed class StartupTask : IBackgroundTask
    {
        IStorage storage;
        IObdDevice elmDevice;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            this.storage = new FileStorage();
            this.elmDevice = new Elm();

            //devdevice = new ElmWifi("192.168.2.147", 36000);

            // Local testing
            var communicator = new SocketCommunicator("192.168.2.147", 36000);
            // Real ELM Device
            //var communicator = new SocketCommunicator("192.168.0.10", 35000);

            this.elmDevice.SetCommicatorDevice(communicator);
            this.elmDevice.Connect();

            // Perform a loop and read continously the data.
            while (true)
            {

                this.Watchdog();
            }
        }


        public async void Watchdog()
        {
            while (true)
            {

                ElmDecoder decoder = null;

                await this.elmDevice.SendAndReceive("ATZ");

                // Get the ODO Meter value
                if (this.elmDevice.IsConnected)
                    decoder =await elmDevice.SendAndReceive(ElmCommands.GetODOmeter);


                if (decoder != null)
                {
                    int i;
                    IInterfaceData interfaceData = new InterfaceData("OBD-Device-1");

                    if (int.TryParse(decoder.DecodedValue, out i))
                        interfaceData.ODOMeter = i;

                    //Maybe it can be, that the decoder resulst some weird values, so thie could not be "decoded".
                    if (!decoder.HasError)
                    {
                        // Write data into store.
                        storage.WriteData(
                           string.Format("stamp:{3};DeviceID:{2};ODO:{0};ERR:{1}",
                           interfaceData.ODOMeter,
                           interfaceData.ErrorCode,
                           interfaceData.DeviceID,
                           interfaceData.Stamp)
                           );

                        // Send the Data to the Azure Hub (if available).
                        await AzureIoTHub.SendEvent(interfaceData);
                    }
                }

            }
        }
    }
}
