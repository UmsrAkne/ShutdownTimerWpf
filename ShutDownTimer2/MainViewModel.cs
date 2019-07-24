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

        public delegate void WarningDisplaying();
        public event WarningDisplaying warningDisplaying;

        private DispatcherTimer timer;

        private int remainingCounter = 60 * 60 * 3;

        private String bindingDate = DateTime.MinValue.AddHours(3).AddYears(1).ToString("HH:mm:ss");
        public String BindingDate {
            get {
                return bindingDate;
            }
            set {
                bindingDate = value;
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

            timer.Tick += (sender , e) => {
                remainingCounter -= 1;
                if(remainingCounter <= 0) {
                    shutDown();
                    timer.Stop();
                    return;
                }

                //  残り時間が１５分を切ったら警告イベントを発行する
                if(remainingCounter == 60 * 15) {
                    warningDisplaying();
                }   

                BindingDate = DateTime.MinValue.AddSeconds(remainingCounter).ToString("HH:mm:ss");
            };

            timer.Start();
        }

        public void addTime( int additionSec ) {
            if(remainingCounter + additionSec < 60 * 10) {
                remainingCounter = 60 * 10;
                return;
            }

            remainingCounter += additionSec;
        }

        /// <summary>
        /// 実行するとこのPCを数十秒後にシャットダウンします。
        /// </summary>
        public void shutDown() {

            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo();

            psi.FileName = "shutdown.exe";
            //コマンドラインを指定
            psi.Arguments = "/s";
            //ウィンドウを表示しないようにする
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
        }
    }
}
