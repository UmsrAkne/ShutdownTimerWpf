using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShutDownTimer2 {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        private MainViewModel mainViewModel;

        public MainWindow() {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            mainViewModel.warningDisplaying += () => {
                Topmost = true;
                MessageBox.Show("残り１５分を切りました");
                Topmost = false;
            };

            this.DataContext = mainViewModel;
        }

        private void ButtonClicked(object sender, RoutedEventArgs e) {
            Button button = (Button)e.Source;
            switch (button.Name) {
                case "plus2hButton":
                    mainViewModel.addTime(60 * 60 * 2);
                    break;
                case "plus1hButton":
                    mainViewModel.addTime(60 * 60);
                    break;
                case "plus30mButton":
                    mainViewModel.addTime(60 * 30);
                    break;
                case "plus10mButton":
                    mainViewModel.addTime(60 * 10);
                    break;

                case "minus2hButton":
                    mainViewModel.addTime(60 * 60 * 2 * -1);
                    break;
                case "minus1hButton":
                    mainViewModel.addTime(60 * 60 * -1);
                    break;
                case "minus30mButton":
                    mainViewModel.addTime(60 * 30 * -1);
                    break;
                case "minus10mButton":
                    mainViewModel.addTime(60 * 10 * -1);
                    break;
            }
        }
    }
}
