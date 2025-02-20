using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Timers;

namespace Marafon
{
    public partial class MainWindow : Window
    {
        private TextBlock _countdownText;
        private TextBlock _currentDateText;
        private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();

            this.AttachDevTools();
            _countdownText = this.FindControl<TextBlock>("CountdownText");
            _currentDateText = this.FindControl<TextBlock>("CurrentDate");

            _currentDateText.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            UpdateCountdown();

            Timer timer = new Timer(60000); // Обновление каждую минуту
            timer.Elapsed += (s, e) => UpdateCountdown();
            timer.Start();
        }

        private void UpdateCountdown()
        {
            var now = DateTime.Now;
            var timeLeft = MarathonDate - now;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _countdownText.Text = $"{timeLeft.Days} дней {timeLeft.Hours} часов {timeLeft.Minutes} минут до старта марафона!";
            });
        }
        private void NewSponsor(object sender, RoutedEventArgs e)
        {
            new RunnerSponsor().Show();
            Close();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}