using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Exception
{
    public class TooOldBirthDateException : FormatException
    {
        public TooOldBirthDateException() : base("Дата народження надто далека в минулому. Людина, ймовірно, не жива.") { }
    }
}
