using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDISample.Storage
{
    public interface IStorage
    {
        void Init();

        void WriteData(string data);


        Task<string> GetAllData();

    }
}
