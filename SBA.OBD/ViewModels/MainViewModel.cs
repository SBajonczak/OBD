using SBA.OBD.Dongle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SBA.OBD.ViewModels
{
    public class MainViewModel:BaseObject
    {
        /// <summary>
        /// The ODO Meter
        /// </summary>
        public string ODO { get { return _odo; } set
            {
                _odo = value;
                OnPropertyChanged("ODO");
            }
        }

        private string _odo;

        public bool Mil { get; set; }


        DispatcherTimer Tmr;
        ODBExecutor executor;

        public MainViewModel()
        {
            executor = new ODBExecutor();

            this.Tmr = new DispatcherTimer();
            Tmr.Interval = new TimeSpan(0, 0, 0, 2);
            Tmr.Tick += TimerElapsed;
            Tmr.Start();
        }

        private void TimerElapsed(object sender, object e)
        {
            if (executor != null)
            {
                executor.Execute();
                this.ODO = executor.ODO;

            }
        }
    }
}
