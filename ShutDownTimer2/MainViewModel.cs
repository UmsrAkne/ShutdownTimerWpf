using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ShutDownTimer2 {
    class MainViewModel : System.ComponentModel.INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged = null;

        private DispatcherTimer timer;

        private String bindingDate = "日時が入ります";
        public String BindingDate {
            get {
                return this.bindingDate;
            }
            set {
                this.bindingDate = value;
                this.OnPropertyChanged(nameof(BindingDate));
                return;
            }
        }

        protected void OnPropertyChanged(String info) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public MainViewModel() {
            setupTimer();
        }

        private void setupTimer() {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (sender , e) => { System.Diagnostics.Trace.WriteLine("test"); };
            timer.Start();
        }
    }
}
