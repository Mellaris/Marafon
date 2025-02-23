using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Marafon.Context;
using Marafon.Models;
using System;
using System.Linq;
using System.Timers;

namespace Marafon;

public partial class RegisterMarathon : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();
    public RegisterMarathon()
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

        UpdateCountdown();

        Timer timer = new Timer(60000); // Обновление каждую минуту
        timer.Elapsed += (s, e) => UpdateCountdown();
        timer.Start();
        LoadRunners();
    }
    private void LoadRunners()
    {
        var runners = DbContext.Charities
            .Select(user => new Charity
            {
                Charityid = user.Charityid,
                Charityname = user.Charityname,
            })
            .ToList();

        charityComboBox.ItemsSource = runners;
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
        new CheckRegistr().Show();
        Close();
    }
}