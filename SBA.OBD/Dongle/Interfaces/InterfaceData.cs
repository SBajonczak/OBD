using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{
    public class InterfaceData : IInterfaceData
    {

        public InterfaceData(string deviceID)
        {
            this.Stamp = DateTime.UtcNow;
            this.DeviceID = deviceID;
        }

        public string DeviceID
        {
            get;private set;
        }

        public string ErrorCode
        {
            get; set;
        }
    
        public int ODOMeter
        {
            get; set;

        }

        public DateTime Stamp
        {
            get; private set;
        }



        public override string ToString()
        {
            return string.Format("stamp:{3};DeviceID:{2};ODO:{0};ERR:{1}",
                           this.ODOMeter,
                           this.ErrorCode,
                           this.DeviceID,
                           this.Stamp);
        }
    }
}
