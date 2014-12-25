using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DaruTweet
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private void GetTweet()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            var result = client.DownloadString("http://api.daruyanagi.info/1/tweet");

            textBlock.Text = result.Replace("\"", "");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Top = SystemParameters.WorkArea.Height - Height;
            Left = SystemParameters.WorkArea.Width - Width;

            GetTweet();

            _timer.Tick += (_, __) =>
            {
                GetTweet();
            };

            _timer.Interval = TimeSpan.FromSeconds(15);
            _timer.Start();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetTweet();
        }
    }
}
