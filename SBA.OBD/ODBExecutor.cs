using Windows.ApplicationModel.Background;
using SBA.OBD.Dongle.Devices;
using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Helpers;
using SBA.OBD.Dongle.Helpers.Enumerations;
using SBA.ODB.Storage.Storage;
using SBA.ODB.Storage.Hubs;
using System;
using SBA.OBD.Dongle.Decoders;

namespace SBA.OBD
{
    public class ODBExecutor 
    {
        IStorage storage;
        IObdDevice elmDevice;

        Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

        public ODBExecutor()
        {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            storage = new FileStorage();
            this.elmDevice = DeviceFactory.Create(DeviceFactory.DeviceType.ELM);


            // Real ELM Device
            TelemetryClient.TrackTrace("Initialize ELM Connection");

            var communicator = CommunicatorFactory.Create(CommunicatorFactory.CommunicatorType.Wifi_ELM_327);

            this.elmDevice.SetCommicatorDevice(communicator);
            TelemetryClient.TrackTrace("DO Connect");
            try
            {
                this.elmDevice.Connect();
                TelemetryClient.TrackTrace("DO Connect Suceed");
            }
            catch (Exception e)
            {
                TelemetryClient.TrackException(e);
            }
        }


        public string ODO { get; set; }


        public async void Execute()
        {
            TelemetryClient.TrackTrace("Pooling Data");
            IDecoder decoder = null;
            await this.elmDevice.SendAndReceive("ATZ");
            // Get the ODO Meter value
            if (this.elmDevice.IsConnected)
                decoder = await elmDevice.SendAndReceive(ElmCommands.GetODOmeter);
            if (decoder != null)
            {
                int i;
                IInterfaceData interfaceData = new InterfaceData("OBD-Device-1");

                if (int.TryParse(decoder.Value, out i))
                    interfaceData.ODOMeter = i;

                //Maybe it can be, that the decoder resulst some weird values, so thie could not be "decoded".
                if (!decoder.HasError)
                {
                    TelemetryClient.TrackTrace("Got Valid Data pushing it to the file");
                    // Write data into store.
                    storage.WriteData(interfaceData.ToString());

                    // Send the Data to the Azure Hub (if available).
                    TelemetryClient.TrackTrace("Pushing it to the IOT Hub");
                    await AzureIoTHub.SendEvent(interfaceData);
                }
            }
        }
    }
}
