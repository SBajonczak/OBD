using System;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.Foundation;
using System.Threading;

namespace SBA.OBD.Dongle.Communicator
{
    public class SocketCommunicator:BaseObject, ICommunicator
    {

        public bool IsConnected { get; private set; }

        public bool IsReady { get; private set; }

        private string _lastMessage;

        public string LastMessage
        {
            get
            {
                return _lastMessage;
            }
            private set
            {
                _lastMessage = value; OnPropertyChanged();
            }
        }


        StreamSocket clientSocket;
        HostName hostName;

        string ServiceName;
        public SocketCommunicator(string ipAdress, int port)
        {
            hostName = new HostName(ipAdress);
            ServiceName = port.ToString();
            this.clientSocket = new StreamSocket();
            this.IsReady = true;
        }


        /// <summary>
        /// Attemt to Connect.
        /// </summary>
        public async Task Connect()
        {
            if (this.IsConnected)
                return;
            CancellationTokenSource cts = new CancellationTokenSource();
            //In this case, after 2 seconds... signal cancel
            cts.CancelAfter(3000);
            IAsyncAction action =this.clientSocket.ConnectAsync(hostName, ServiceName);
            var connectTask = action.AsTask(cts.Token);
            try {
                await connectTask;
            }
            catch
            {
                // Only cancelation occurs
            }

            if(action.Status == AsyncStatus.Completed)
                this.IsConnected= true;
        }


        /// <summary>
        /// Receive data from the stream.
        /// </summary>
        /// <returns></returns>
        private async Task<string> Receive()
        {
            DataReader reader = new DataReader(clientSocket.InputStream);
            // Set inputstream options so that we don't have to know the data size
            reader.InputStreamOptions = InputStreamOptions.Partial;
            while (true)
            {
                
                var bytesAvailable = await reader.LoadAsync(1000);
                if (bytesAvailable > 0)
                    return reader.ReadString(bytesAvailable);
            }
        }

        private async void Send(string data)
        {
            if (IsConnected==false)
            this.IsReady = false;
            string sendData = data + Environment.NewLine;
            DataWriter writer = new DataWriter(clientSocket.OutputStream);
            uint len = writer.MeasureString(sendData); // Gets the UTF-8 string length.
            try
            {

                writer.WriteString(sendData);
                // Call StoreAsync method to store the data to a backing stream

                await writer.StoreAsync();
                // detach the stream and close it
                writer.DetachStream();
                writer.Dispose();

            }
            catch (Exception e)
            {

                if (e.Message.Contains("An existing connection was forcibly closed by the remote host."))
                {
                    this.IsConnected = false;
                }
            }
            finally
            {
                this.IsReady = true;
            }
        }

        public async Task<string> SendAndReceive(string message)
        {
            if (this.IsReady)
            {
                try {
                    this.IsReady = false;
                    string result = string.Empty;
                    this.Send(message);
                    result = await this.Receive();
                    this.IsReady = true;

                    return result;
                }catch(Exception e)
                {
                    if (e.Message.Contains("The object has been closed."))
                        this.IsConnected = false;
                }
                finally
                {
                    this.IsReady = true;
                }
            }
            return "NotReady";
        }


        private void Disconnect()
        {
            this.IsConnected = false;
            this.clientSocket.Dispose();

        }
    }

  
}
