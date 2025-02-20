using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Timers;

namespace Marafon;

public partial class DetailedInformation : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public DetailedInformation()
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

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

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new CharitableOrganizations().Show();
        Close();
    }
}