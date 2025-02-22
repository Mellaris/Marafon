using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Marafon.Context;
using Marafon.Models;
using System.Timers;

namespace Marafon;

public partial class LogIn : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();
    public LogIn()
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
        var runners = DbContext.Users
           .Select(user => new User
           {
               Firstname = user.Firstname,
               Lastname = user.Lastname,
               Email = user.Email,
               Userpassword = user.Userpassword,
               Id = user.Id,
               Roleid = user.Roleid,
           })
           .ToList();

        foreach (User runner in runners)
        {
            if (mail.Text == runner.Email && passw.Text == runner.Userpassword)
            {
                StaticHelp.role = runner.Roleid.ToString();
                if (StaticHelp.role == "R")
                {
                    new RunnerMenu().Show();
                    Close();
                    break;
                }
                else if (StaticHelp.role == "C")
                {
                    new CoordinatorMenu().Show();
                    Close();
                    break;
                }
                else if (StaticHelp.role == "A")
                {
                    new AdminMenu().Show();
                    Show();
                    break;
                }
            }
            else
            {
                textM.Text = "Пароль или почта не подходит";
            }
        }
    }
}