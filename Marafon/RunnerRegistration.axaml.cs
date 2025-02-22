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

namespace Marafon;

public partial class RunnerRegistration : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public static User9Context DbContext { get; set; } = new User9Context();
    public RunnerRegistration()
    {
        InitializeComponent();
        this.AttachDevTools();
        _countdownText = this.FindControl<TextBlock>("CountdownText");

        UpdateCountdown();

        Timer timer = new Timer(60000); // ���������� ������ ������
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
            MessageBox.Text = "��� ���� ����������� ��� ����������.";
            return false;
        }

        if (!Regex.IsMatch(EmailBox.Text, @"^[^@]+@[^@]+\.[^@]+$"))
        {
            MessageBox.Text = "�������� ������ e-mail.";
            return false;
        }

        if (!Regex.IsMatch(PasswordBox.Text, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^]).{6,}$"))
        {
            MessageBox.Text = "������ ������ ��������� ������� 6 ��������, 1 ��������� �����, 1 ����� � 1 ����������� ������ (!@#$%^).";
            return false;
        }

        if (PasswordBox.Text != ConfirmPasswordBox.Text)
        {
            MessageBox.Text = "������ �� ���������.";
            return false;
        }

        if (!DateTime.TryParse(BirthDateBox.Text, out DateTime birthDate))
        {
            MessageBox.Text = "������������ ���� ��������.";
            return false;
        }

        if ((DateTime.Now - birthDate).TotalDays / 365.25 < 10)
        {
            MessageBox.Text = "������ ������ ���� �� ����� 10 ���.";
            return false;
        }

        return true;
    }
    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        if (ValidateFields())
        {
            MessageBox.Text = "����������� �������!";
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
            _countdownText.Text = $"{timeLeft.Days} ���� {timeLeft.Hours} ����� {timeLeft.Minutes} ����� �� ������ ��������!";
        });
    }
}