using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2
{
    internal class NullBirthDateException : System.Exception
    {
        public NullBirthDateException() : base("Дата народження не може бути null.")
        {
        }
    }
}