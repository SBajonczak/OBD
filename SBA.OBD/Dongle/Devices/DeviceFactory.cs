using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Devices
{
    public class DeviceFactory
    {

        public enum DeviceType
        {
            ELM
        }

        /// <summary>
        /// Create a new <see cref="IObdDevice"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IObdDevice Create(DeviceType type)
        {
            switch (type)
            {
                case DeviceType.ELM:
                    return new Elm();
                default:
                    throw new NotImplementedException("Sorry not implemented yet");
            }
        }
    }
}
