using Lab2.Exception;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
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
        /*public MainWindow()
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
            DateTime? birthDate = BirthDatePicker.SelectedDate;

            try
            {
                if (birthDate == null)
                {
                    throw new NullBirthDateException();
                }

                Person person = await Task.Run(() => new Person(firstName, lastName, email, birthDate.Value));

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
            catch (NullBirthDateException ex)
            {
                MessageBox.Show("Будь ласка, виберіть дату народження.");
            }
            catch (System.Exception ex)
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

            try
            {
                IsValidEmail(EmailBox.Text);
            }
            catch (InvalidEmailFormatException ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
                return false;
            }

            DateTime birthDate = BirthDatePicker.SelectedDate.Value;
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age)) age--;

            try
            {
                if (age > 135)
                {
                    throw new TooOldBirthDateException();
                }

                if (birthDate > DateTime.Now)
                {
                    throw new FutureBirthDateException();
                }
            }
            catch (TooOldBirthDateException ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
                return false;
            }
            catch (FutureBirthDateException ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
                return false;
            }

            return true;
        }



        private void IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new InvalidEmailFormatException();
            }
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
        }*/

        public MainWindow()
        {
            InitializeComponent();
            LoadUsers();
            dataGrid.ItemsSource = users;
        }

        private ObservableCollection<Person> users;

        private void LoadUsers()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                users = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);
            }
            else
            {
                users = new ObservableCollection<Person>();
                for (int i = 0; i < 50; i++)
                {
                    users.Add(new Person($"FirstName{i}", $"LastName{i}", $"email{i}@domain.com", DateTime.Now.AddYears(-20).AddDays(i)));
                }
                SaveUsers();
            }
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", json);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new Person("NewFirstName", "NewLastName", "newemail@domain.com", DateTime.Now.AddYears(-25));
            users.Add(newUser);
            SaveUsers();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Person selectedUser)
            {
                selectedUser.FirstName = "EditedFirstName";
                selectedUser.LastName = "EditedLastName";
                selectedUser.Email = "editedemail@domain.com";
                selectedUser.BirthDate = DateTime.Now.AddYears(-30);
                dataGrid.Items.Refresh();
                SaveUsers();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Person selectedUser)
            {
                users.Remove(selectedUser);
                SaveUsers();
            }
        }
    }
}
