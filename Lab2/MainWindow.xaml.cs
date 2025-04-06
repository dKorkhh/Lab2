using Lab2.Exception;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FirstNameBox.GotKeyboardFocus += GotKeyboardFocus;
            FirstNameBox.LostKeyboardFocus += LostKeyboardFocus;
            FirstNameBox.TextChanged += ValidateInput;

            LastNameBox.GotKeyboardFocus += GotKeyboardFocus;
            LastNameBox.LostKeyboardFocus += LostKeyboardFocus;
            LastNameBox.TextChanged += ValidateInput;

            EmailBox.GotKeyboardFocus += GotKeyboardFocus;
            EmailBox.LostKeyboardFocus += LostKeyboardFocus;
            EmailBox.TextChanged += ValidateInput;

            BirthDatePicker.SelectedDateChanged += ValidateInput;

            ProceedButton.IsEnabled = false;
        }

        private void ValidateInput(object sender, EventArgs e)
        {
            ProceedButton.IsEnabled = !string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                                      !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                                      !string.IsNullOrWhiteSpace(EmailBox.Text) &&
                                      BirthDatePicker.SelectedDate.HasValue &&
                                      FirstNameBox.Text != "Ім'я" &&
                                      LastNameBox.Text != "Прізвище" &&
                                      EmailBox.Text != "Пошта";
        }

        private async void Proceed_Click(object sender, RoutedEventArgs e)
        {
            ProceedButton.IsEnabled = false;

            if (!ValidateForm())
            {
                ProceedButton.IsEnabled = true;
                return;
            }

            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;
            string email = EmailBox.Text;
            DateTime birthDate = BirthDatePicker.SelectedDate.Value;

            try
            {
                Person person = await Task.Run(() => new Person(firstName, lastName, email, birthDate));

                if (person.IsBirthday)
                {
                    MessageBox.Show("З днем народження!");
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ResultWindow resultWindow = new ResultWindow(person);
                    resultWindow.Show();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
            finally
            {
                ProceedButton.IsEnabled = true;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                !BirthDatePicker.SelectedDate.HasValue ||
                FirstNameBox.Text == "Ім'я" ||
                LastNameBox.Text == "Прізвище" ||
                EmailBox.Text == "Пошта")
            {
                MessageBox.Show("Заповніть всі поля!");
                return false;
            }

            if (!IsValidEmail(EmailBox.Text))
            {
                MessageBox.Show("Неправильна пошта");
                return false;
            }

            DateTime birthDate = BirthDatePicker.SelectedDate.Value;
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age)) age--;

            if (age > 135)
            {
                throw new TooOldBirthDateException();
            }

            if (birthDate > DateTime.Now)
            {
                throw new FutureBirthDateException();
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                if (textBox == FirstNameBox) textBox.Text = "Ім'я";
                if (textBox == LastNameBox) textBox.Text = "Прізвище";
                if (textBox == EmailBox) textBox.Text = "Пошта";
            }
        }
    }
}
