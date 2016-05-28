using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SBA.OBD.Dongle
{
    public class BaseObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                if (PropertyChanged!= null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch
            {

            }
        }

    }
}
