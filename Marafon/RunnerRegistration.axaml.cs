using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Linq;
using Marafon.Context;
using Marafon.Models;
using System.Timers;
using System.Text.RegularExpressions;
using Avalonia.Interactivity;
using Metsys.Bson;
using System.Xml.Linq;

namespace Marafon;

public partial class RunnerRegistration : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();

    public int indexGender;
    public string textGender;
    public int indexCountry;
    public string textCountry;
    public RunnerRegistration()
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

        UpdateCountdown();

        Timer timer = new Timer(60000); // Обновление каждую минуту
        timer.Elapsed += (s, e) => UpdateCountdown();
        timer.Start();
        GenderCall();
        CountryCall();
    }
    private bool ValidateFields()
    {
        if (string.IsNullOrWhiteSpace(EmailBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordBox.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordBox.Text) ||
            string.IsNullOrWhiteSpace(NameBox.Text) ||
            string.IsNullOrWhiteSpace(SurnameBox.Text) ||
            GenderBox.SelectedItem == null ||
            countryBox.SelectedItem == null ||
            string.IsNullOrWhiteSpace(BirthDateBox.Text))
        {
            MessageBox.Text = "Все поля обязательны для заполнения.";
            return false;
        }

        if (!Regex.IsMatch(EmailBox.Text, @"^[^@]+@[^@]+\.[^@]+$"))
        {
            MessageBox.Text = "Неверный формат e-mail.";
            return false;
        }

        if (!Regex.IsMatch(PasswordBox.Text, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^]).{6,}$"))
        {
            MessageBox.Text = "Пароль должен содержать минимум 6 символов, 1 заглавную букву, 1 цифру и 1 специальный символ (!@#$%^).";
            return false;
        }

        if (PasswordBox.Text != ConfirmPasswordBox.Text)
        {
            MessageBox.Text = "Пароли не совпадают.";
            return false;
        }

        if (!DateTime.TryParse(BirthDateBox.Text, out DateTime birthDate))
        {
            MessageBox.Text = "Некорректная дата рождения.";
            return false;
        }

        if ((DateTime.Now - birthDate).TotalDays / 365.25 < 10)
        {
            MessageBox.Text = "Бегуну должно быть не менее 10 лет.";
            return false;
        }

        return true;
    }
    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        char role = 'R';
        if (ValidateFields())
        {
            User runner = new User
            {
                Id = DbContext.Users.OrderBy(x => x.Id).LastOrDefault().Id + 1,
                Email = EmailBox.Text,
                Userpassword = PasswordBox.Text,
                Firstname = NameBox.Text,
                Lastname = SurnameBox.Text,
                Roleid = role,

            };
            using (var a = new User9Context())
            {
                a.Add(runner);
                a.SaveChanges();
            }


            Runner run = new Runner
            {
                Runnerid = DbContext.Runners.OrderBy(x => x.Runnerid).LastOrDefault().Runnerid + 1,
                Email = EmailBox.Text,
                Dateofbirth = Convert.ToDateTime(BirthDateBox.Text),
                Gender = textGender,
                Countrycode = textCountry,
            };
            using (var a = new User9Context())
            {
                a.Add(run);
                a.SaveChanges();
            }
            new LogIn().Show();
            Close();
        }
    }
    private void Stop(object sender, RoutedEventArgs e)
    {
        new CheckingRunner().Show();
        Close();
    }
    private void Back(object sender, RoutedEventArgs e)
    {
        new CheckingRunner().Show();
        Close();
    }
    private void GenderCall()
    {
        var runners = DbContext.Genders
            .Select(user => new Gender
            {
                Gender1 = user.Gender1,
                Id = user.Id,
            })
            .ToList();

        GenderBox.ItemsSource = runners;
    }
    private void CountryCall()
    {
        var runners = DbContext.Countries
           .Select(user => new Country
           {
               Countryname = user.Countryname,
               Id = user.Id,
           })
           .ToList();

        countryBox.ItemsSource = runners;
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

    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        indexGender = GenderBox.SelectedIndex;
        if(indexGender == 0)
        {
            textGender = "Female";
        }
        else
        {
            textGender = "Male";
        }
    }

    private void ComboBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        indexCountry = countryBox.SelectedIndex;
        indexCountry = indexCountry + 1;
        var runners = DbContext.Countries
           .Select(user => new Country
           {
               Countryname = user.Countryname,
               Countrycode = user.Countrycode,
               Id = user.Id,
           })
           .ToList();
        foreach (Country a in runners)
        {
            if(indexCountry == a.Id)
            {
                textCountry = a.Countrycode;
                break;
            }
        }
    }
}