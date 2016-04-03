using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA.ODB.Storage.Storage
{
    public interface IStorage
    {
        void Init();

        void WriteData(string data);


        Task<string> GetAllData();

    }
}
