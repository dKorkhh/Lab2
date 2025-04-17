using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab2.GridForm
{
    /// <summary>
    /// Interaction logic for AllUserWindow.xaml
    /// </summary>
    public partial class AllUserWindow : Window
    {
        public AllUserWindow()
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
