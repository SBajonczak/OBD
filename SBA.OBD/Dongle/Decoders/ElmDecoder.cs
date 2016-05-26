using SBA.OBD.Dongle.Decoders;
using SBA.OBD.Dongle.Helpers.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.OBD.Dongle.Decoders
{
    public class ElmDecoder: IDecoder
    {

        public int SucessCode { get; private set; }

        public int Mode { get; private set; }

        public string Pid{ get; private set; }

        public string[] Chunks { get; private set; }

        public string ResultValue { get; private set; }

        public string Value { get; private set; }

        public bool HasError { get; private set; }

        public ElmDecoder(string input) {
            if (string.IsNullOrEmpty(input))
            {
                this.HasError = true;
                return;
            }
            this.Decode(input);
        }

        /// <summary>
        /// Decode the values.
        /// </summary>
        /// <param name="result"></param>
        public void Decode(string result)
        {
            this.ResultValue = result;

            ParseChunks();
            if (HasError == false)
                Decode();
        }

        private void ParseChunks()
        {
            try
            {
                var integerChunks = ResultValue.Replace(">", "").Replace("\r", string.Empty).Split(' ');
                SucessCode = int.Parse(integerChunks[0]);

                // Get the Mode. by subtracting 40;
                this.Mode = SucessCode - 40;

                Pid = integerChunks[1];

                this.Chunks = integerChunks.Skip(2).ToArray();
            }
            catch (Exception)
            {
                this.HasError = true;
            }
        }

        private void Decode()
        {
            switch (Pid.GetIntegerValue()){

                case (int)ElmCommands.Mode1COmmands.DistanceTraveledSinceLastReset:
                    this.Value= (((Chunks[0].GetIntegerValue() * 256) + Chunks[1].GetIntegerValue())).ToString();
                    break;
                case (int)ElmCommands.Mode1COmmands.DistanceTraveledWithMIL:
                    this.Value = (((Chunks[0].GetIntegerValue() * 256) + Chunks[1].GetIntegerValue())).ToString();
                    break;
                case (int)ElmCommands.Mode1COmmands.EngineRPM:
                    this.Value = (((Chunks[0].GetIntegerValue() * 256) + Chunks[1].GetIntegerValue()) / 4).ToString();
                    break;
                case (int)ElmCommands.Mode1COmmands.FuelSystemStatus:
                    break;
                case (int)ElmCommands.Mode1COmmands.FuelType:
                    break;
                case (int)ElmCommands.Mode1COmmands.MonitorStatus:
                    break;
                case (int)ElmCommands.Mode1COmmands.SupportedPids0To20:
                    break;
                case (int)ElmCommands.Mode1COmmands.VehicleSpeed:
                    this.Value = (Chunks[0].GetIntegerValue()).ToString();
                    break;
            }
        }

        /// <summary>
        /// Get Distance Value;
        /// </summary>
        /// <returns></returns>
        public float GetDistance()
        {
            return (Chunks[0].GetIntegerValue() * 256) + Chunks[1].GetIntegerValue();
        }
    }

    public static class StringExtensions
    {
        public static int GetIntegerValue(this string input)
        {
            return int.Parse(input, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
