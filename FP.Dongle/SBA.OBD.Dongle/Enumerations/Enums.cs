using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Helpers.Enumerations
{
    /// <summary>
    /// Setting the Protocoll
    /// </summary>
    public enum Protocol
    {
        Automatic = 0,
        J1850_PWM = 1,
        J1850_VPW = 2,
        ISO_9141_2 = 3,
        ISO14230_KWP_5BaudInit = 4,
        ISO14230_KWP_FastInit = 5,
        ISO15765_4_CAN_11BIT = 6,
        ISO15765_4_CAN_29BIT = 7
    }


    /// <summary>
    /// Set the headers.
    /// </summary>
    public enum Headers
    {
        ON = 1,
        OFF = 0
    }

    /// <summary>
    /// The Baudrates.
    /// </summary>
    public enum BaudRate
    {
        B38400 = 38400,
        B9600 = 9600
    }

    public class ElmCommands
    {
        /// <summary>
        /// Command to Get the FuelState
        /// </summary>
        public static readonly string GetFuelState="010A";

        /// <summary>
        /// Command to get the ODO Meter Amount
        /// </summary>
        public static readonly string GetODOmeter = "0131";


        /// <summary>
        /// Command to get the ODO Meter Amount
        /// </summary>
        public static readonly string GetFaultCodes = "03";

        /// <summary>
        /// Command to get the ODO Meter Amount
        /// </summary>
        public static readonly string Reset = "ATZ";

        public static Dictionary<string,string> GetDataCollection()
        {
            var result = new Dictionary<string, string>();

            result.Add("RESET", Reset);
            result.Add("FuelState", GetFuelState);
            result.Add("ODO Meter", GetODOmeter);
            result.Add("Error Codes", GetFaultCodes);
            return result;
        }


        public enum Mode1COmmands : short
        {
            /// <summary>
            /// Result 4 Bytes
            /// </summary>
            SupportedPids0To20=0x00,

            /// <summary>
            /// Resulting 4 Bytes
            /// </summary>
            MonitorStatus = 0x01,

            /// <summary>
            /// Resulting 2 Bytes
            /// </summary>
            FuelSystemStatus= 0x03,

            /// <summary>
            /// Result 2 Bytes Formular (((A*256)+B)/4
            /// </summary>
            EngineRPM =0x0C,

            /// <summary>
            /// Resulting 1 Byte cat it as Integer
            /// </summary>
            VehicleSpeed=0x0D,

            /// <summary>
            /// Resulting 2 Bytes Formular (a*256)+B
            /// </summary>
            DistanceTraveledWithMIL=0x21,
            /// <summary>
            /// Resulting 2 Bytes Formular (a*256)+B
            /// </summary>
            DistanceTraveledSinceLastReset = 0x31,

            /// <summary>
            /// Result 1 Byte
            /// </summary>
            FuelType = 0x52,

        }
    }



}
