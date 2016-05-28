using SBA.OBD.Dongle.Devices;
using SBA.OBD.Dongle.Communicator;
using SBA.OBD.Dongle.Helpers.Enumerations;
using SBA.ODB.Storage.Hubs;
using System;
using SBA.OBD.Dongle.Decoders;
using SBA.ODB.Storage;
using System.Threading.Tasks;

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

            var communicator = CommunicatorFactory.Create(CommunicatorFactory.CommunicatorType.DEVELOP_FAKESERVER);

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
        public bool MIL { get; set; }

        public async Task<IDecoder> Execute (string command)
        {
            IDecoder decoder = null;
            if (this.elmDevice.IsConnected)
                decoder = await elmDevice.SendAndReceive(command);
            if (decoder != null)
            {
                int i;
                IInterfaceData interfaceData = new InterfaceData("OBD-Device-1");

                if (int.TryParse(decoder.Value, out i))
                    interfaceData.ODOMeter = i;

                this.ODO = interfaceData.ODOMeter.ToString();
                //Maybe it can be, that the decoder resulst some weird values, so thie could not be "decoded".
                if (!decoder.HasError)
                {
                    TelemetryClient.TrackTrace("Got Valid Data pushing it to the file");
                    // Write data into store.
                    storage.WriteData(interfaceData.ToString());

                    try
                    {
                        // Send the Data to the Azure Hub (if available).
                        TelemetryClient.TrackTrace("Pushing it to the IOT Hub");
                        await AzureIoTHub.SendEvent(interfaceData);
                    }
                    catch (Exception ex)
                    {
                        TelemetryClient.TrackException(ex);
                    }
                }
            }
            return decoder;
        }

        public async void Execute()
        {
            TelemetryClient.TrackTrace("Pooling Data");
            int i=0;
            await this.elmDevice.SendAndReceive("ATZ");
            // Get the ODO Meter value
            IDecoder decoder = await Execute(ElmCommands.GetODOmeter);

            if (int.TryParse(decoder?.Value, out i))
                this.ODO = i.ToString();
            IDecoder decoderMil = await Execute(ElmCommands.GetFaultCodes);
            this.MIL = !string.IsNullOrEmpty(decoder?.Value);

        }

    }
}
