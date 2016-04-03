using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace FTDISample.Helpers
{
    public class Ping
    {

        public string ConnectionAttemptInformation { get; private set; }

        public bool ConnectionInProgress
        {
            get; private set;
        }

        public async Task<bool> DoPing(string HostName, string PortNumber)
        {
            bool result=false;
            if (!ConnectionInProgress)
            {

                ConnectionAttemptInformation = "";
                ConnectionInProgress = true;

                try
                {
                    using (var tcpClient = new StreamSocket())
                    {
                        await tcpClient.ConnectAsync(
                            new Windows.Networking.HostName(HostName),
                            PortNumber,
                            SocketProtectionLevel.PlainSocket);

                        var localIp = tcpClient.Information.LocalAddress.DisplayName;
                        var remoteIp = tcpClient.Information.RemoteAddress.DisplayName;

                        ConnectionAttemptInformation = String.Format("Success, remote server contacted at IP address {0}",
                                                                     remoteIp);
                        tcpClient.Dispose();
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147013895)
                    {
                        ConnectionAttemptInformation = "Error: No such host is known";
                    }
                    else if (ex.HResult == -2147014836)
                    {
                        ConnectionAttemptInformation = "Error: Timeout when connecting (check hostname and port)";
                    }
                    else
                    {
                        ConnectionAttemptInformation = "Error: Exception returned from network stack: " + ex.Message;
                    }
                    ConnectionInProgress = false;
                    result = false;

                }
                finally
                {
                    ConnectionInProgress = false;
                }
            }
            return result;
        }
    }
}
