using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using Marafon.Context;
using Marafon.Models;

namespace Marafon;

public partial class CharitableOrganizations : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();
    public CharitableOrganizations()
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

        UpdateCountdown();

        Timer timer = new Timer(60000); // Обновление каждую минуту
        timer.Elapsed += (s, e) => UpdateCountdown();
        timer.Start();
        CallBaza();
    }
    private void CallBaza()
    {
        var runners = DbContext.Charities
            .Select(user => new Charity
            {
                Charityid = user.Charityid,
                Charityname = user.Charityname,
                Charitydescription = user.Charitydescription,
            })
            .ToList();
        charityOrg.ItemsSource = runners;
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
}