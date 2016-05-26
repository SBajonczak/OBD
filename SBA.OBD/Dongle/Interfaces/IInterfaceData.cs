using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{
    /// <summary>
    /// The interface data.
    /// </summary>
    public interface IInterfaceData
    {
        /// <summary>
        /// Die Device ID.
        /// </summary>
        string DeviceID { get; }

        /// <summary>
        /// ODO Meter
        /// </summary>
        int ODOMeter { get; set; }

        /// <summary>
        /// Errorcode
        /// </summary>
        string ErrorCode { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        DateTime Stamp { get;}

    }
}
