using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Marafon.Context;
using Marafon.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;

namespace Marafon;

public partial class RunnerSponsor : Window
{
    private TextBlock _countdownText;
    private static readonly DateTime MarathonDate = new DateTime(2025, 11, 24, 0, 0, 0);
    public int indexSponsor;
    public string cardNumber;
    private readonly User9Context _dbContext;
    public static User9Context DbContext { get; set; } = new User9Context();
    public RunnerSponsor()
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
        var runners = DbContext.Users
            .Where(user => user.Roleid.ToString() == "R")
            .Select(user => new User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
            })
            .ToList();

        runnerComboBox.ItemsSource = runners;
    }
    private void OnIncreaseDonationClick(object sender, RoutedEventArgs e)
    {
        UpdateDonationAmount(10);
    }

    private void OnDecreaseDonationClick(object sender, RoutedEventArgs e)
    {
        UpdateDonationAmount(-10);
    }

    private void UpdateDonationAmount(int change)
    {
        if (int.TryParse(donationAmountTextBox.Text, out int currentAmount))
        {
            currentAmount = currentAmount + (change);
            if (currentAmount < 0) currentAmount = 0; // Не допускаем отрицательные суммы
            donationAmountTextBox.Text = currentAmount.ToString();
        }
        else
        {
            donationAmountTextBox.Text = "0"; // Если введено не число, устанавливаем 0
        }
    }
    private void OnPayButtonClick(object sender, RoutedEventArgs e)
    {
        if(cardNumberTextBox.Text != null)
        {
            cardNumber = cardNumberTextBox.Text.Replace(" ", "");
        }
        else
        {
            cardNumber = "1";
        }
        string cvc = cvcTextBox.Text;
        string expirationMonth = expirationMonthTextBox.Text;
        string expirationYear = expirationYearTextBox.Text;

        if (string.IsNullOrEmpty(donationAmountTextBox.Text) || string.IsNullOrEmpty(cardNumber) ||
            string.IsNullOrEmpty(expirationMonth) || string.IsNullOrEmpty(expirationYear) ||
            string.IsNullOrEmpty(cvc) || runnerComboBox.SelectedItem == null)
        {
            ShowErrorMessage.Text = "Пожалуйста, заполните все поля.";
            return;
        }

        if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
        {
            ShowErrorMessage.Text = "Неверный номер карты. Должен содержать 16 цифр.";
            return;
        }

        if (!Regex.IsMatch(cvc, @"^\d{3}$"))
        {
            ShowErrorMessage.Text = "Неверный CVC. Должен содержать 3 цифры.";
            return;
        }
        if (!int.TryParse(expirationMonth, out int month) || !int.TryParse(expirationYear, out int year) ||
            month < 1 || month > 12 || (year < DateTime.Now.Year || (year == DateTime.Now.Year && month < DateTime.Now.Month)))
        {
            ShowErrorMessage.Text = "Неверный срок действия карты.";
            return;
        }

        // Если все проверки пройдены, выполнить оплату
        ShowErrorMessage.Text = "Пожертвование успешно оформлено!";
        int cost = Convert.ToInt32(donationAmountTextBox.Text);
        new ConfirmationSponsorship(indexSponsor, cost).Show();
        Close();
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
        indexSponsor = runnerComboBox.SelectedIndex;
        indexSponsor = indexSponsor + 1;
    }
}