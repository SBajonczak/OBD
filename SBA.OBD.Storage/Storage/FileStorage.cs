using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SBA.ODB.Storage.Storage
{

    public class FileStorage : IStorage, IDisposable
    {

        StorageFile file;

        private bool IsAccessing;

        public FileStorage()
        {

            this.Init();
            IsAccessing = false;
        }


        public async void Init()
        {
            try
            {
                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                file = await localFolder.CreateFileAsync("Data.ODB",CreationCollisionOption.OpenIfExists);
            }
            catch (Exception e)
            {
            }
        }

        public async Task<string> GetAllData()
        {
            while (IsAccessing)
            {
            }
            IsAccessing = true;
            Stream stream = await file.OpenStreamForReadAsync();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Dispose();
            IsAccessing = false;

            return result;
        }


        public async void WriteData(string data)
        {
            while (IsAccessing)
            {
            }
            IsAccessing = true;
            Stream stream = await file.OpenStreamForWriteAsync();
            stream.Position = stream.Length;
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(data);

            writer.Flush();
            writer.Dispose();
            
            IsAccessing = false;

        }

        public void Dispose()
        {
         
        }
    }
}
