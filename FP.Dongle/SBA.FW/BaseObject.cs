using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SBA.FW
{
    public class BaseObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                if (PropertyChanged.Target==null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {
            }
        }

    }
}
