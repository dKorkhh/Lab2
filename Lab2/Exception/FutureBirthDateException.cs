using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2.Exception
{
    public class FutureBirthDateException : FormatException
    {
        public FutureBirthDateException() : base("Дата народження не може бути в майбутньому.") { 
        }
    }
}
