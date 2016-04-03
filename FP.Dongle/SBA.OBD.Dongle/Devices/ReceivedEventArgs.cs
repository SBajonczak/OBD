namespace SBA.OBD.Dongle.Devices
{
    public class ReceivedEventArgs
    {

        public ReceivedEventArgs(string command, string result)
        {
            this.CommandWasSended = command;
            this.Result = result;
        }


        public string CommandWasSended
        {
            get;private set;
        }

        public string Result
        {
            get; set;
        }

    }
}
