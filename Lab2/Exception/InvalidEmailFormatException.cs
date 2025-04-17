using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2.Exception
{
    public class InvalidEmailFormatException : FormatException
    {
        public InvalidEmailFormatException() : base("Невірний формат електронної пошти.") {
        }
    }
}
