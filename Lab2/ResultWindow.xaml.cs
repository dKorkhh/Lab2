using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Lab2
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(Person person)
        {
            InitializeComponent();

            Firstname.Text = "Ім'я користувача: " + person.FirstName;
            Lastname.Text = "Прізвище користувача: " + person.LastName;
            EmailResult.Text = "Пошта: " + person.Email;
            DateBirth.Text = "Дата народження: " + person.BirthDate.ToShortDateString();

            ZodiacResult.Text = "Традиційний західний знак зодіаку: " + person.ChineseSign;

            ChineseZodiacResult.Text = "Китайський астрологічний знак зодіаку: " + person.SunSign;
            IsAdult.Text = "Людина старша за 18?: " + (person.IsAdult ? "True" : "False");
            IsBirthday.Text = "Чи сьогодні день народження?: " + (person.IsBirthday ? "True" : "False");
        }
    }
}
