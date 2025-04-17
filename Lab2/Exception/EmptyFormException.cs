using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2.Exception
{
    class EmptyFormException : RankException
    {
        public EmptyFormException() : base("Не всі поля заповнені") {
           
        }
    }
}
