using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.DeviceCreate
{
    class Program
    {

        //5X4v034PULDbjiFug83oUzm0PlAYq3V0fe5niGBnhq8=
        static RegistryManager registryManager;
        static string connectionString = "HostName=DataFleet.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=kqNP2zp/pNpn3jf8Mw5UQgjLskQQXhsVcRM7TLhO1Pc=";

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private async static Task AddDeviceAsync()
        {
            string deviceId = "myFirstDevice";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }

    }
}
