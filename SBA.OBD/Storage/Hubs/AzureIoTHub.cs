using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SBA.ODB.Storage.Hubs
{


    public class AzureIoTHub
    {
        // Define the connection string to connect to IoT Hub
        private const string DeviceConnectionString = "";
        private const string DeviceId = "";
        

        public AzureIoTHub()
        {

        }




        /// <summary>
        /// Create a message and send it to IoT Hub.
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        public static async Task SendEvent(object dataBuffer)
        {
            // Erstelle ein Device Objekt zu dem gesendet wird.
            DeviceClient deviceClient = DeviceClient.Create("DataFleet.azure-devices.net",new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", DeviceId),TransportType.Http1);

            var messageString = JsonConvert.SerializeObject(dataBuffer);

            //erstelle das Nachrichten Objekt
            Message eventMessage = new Message(Encoding.UTF8.GetBytes(messageString));
           
            // Und ab die Post
            await deviceClient.SendEventAsync(eventMessage);
        }

        /// <summary>
        /// Receive messages from IoT Hub
        /// </summary>
        /// <returns></returns>
        static async Task ReceiveCommands()
        {

            // Erstelle ein Device Objekt zu dem gesendet wird.
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            Message receivedMessage;
            string messageData;
            while (true)
            {
                receivedMessage = await deviceClient.ReceiveAsync(TimeSpan.FromSeconds(1));

                if (receivedMessage != null)
                {
                    messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    //Console.WriteLine("\t{0}> Received message: {1}", DateTime.Now.ToLocalTime(), messageData);
                    await deviceClient.CompleteAsync(receivedMessage);
                }
            }
        }
    }
}
