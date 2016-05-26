using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Decoders
{
    public interface IDecoder
    {

        /// <summary>
        /// Get the requested mode.
        /// </summary>
        int Mode { get; }

        /// <summary>
        /// Get the requested pid.
        /// </summary>
        string Pid { get; }

        /// <summary>
        /// Indicates if the value could be successfully decoded.
        /// </summary>
        bool HasError { get; }

        /// <summary>
        /// Get the readed value.
        /// </summary>
        string Value { get;  }



    }
}
