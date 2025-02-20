using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Marafon.Context;
using System;
using System.Timers;
using Marafon.Context;
using Marafon.Models;
using System.Linq;

namespace Marafon;

public partial class ConfirmationSponsorship : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();
    int indexRun;
    string emailRun;
    string country;
    public ConfirmationSponsorship()
    {
        InitializeComponent();
    }
    public ConfirmationSponsorship(int index, int cost)
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

        UpdateCountdown();

        Timer timer = new Timer(60000); // Обновление каждую минуту
        timer.Elapsed += (s, e) => UpdateCountdown();
        timer.Start();

        indexRun = index;
        LoadRunners();
        sum.Text = cost.ToString();

    }
    private void LoadRunners()
    {
        var runners = DbContext.Users
            .Where(user => user.Id == indexRun)
            .Select(user => new User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Id = user.Id
            })
            .ToList();
        foreach (User user in runners)
        {
            emailRun = user.Email;
            name.Text = user.Firstname;
            lastname.Text = user.Lastname;
            break;
        }
        var runner = DbContext.Runners
            .Where(user => user.Email == emailRun)
            .Select(user => new Runner
            {
                Runnerid = user.Runnerid,
                Countrycode = user.Countrycode,
            }).ToList();
        foreach(Runner user in runner)
        {
            country = user.Countrycode;
            kod.Text = user.Runnerid.ToString();
            break;
        }
        var coutries = DbContext.Countries
            .Where(user => user.Countrycode == country)
            .Select(user => new Country
            {
                Countrycode = user.Countrycode,
                Countryname = user.Countryname,
            }).ToList();
        foreach(Country country in coutries)
        {
            contr.Text = country.Countryname;
            break;
        }
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