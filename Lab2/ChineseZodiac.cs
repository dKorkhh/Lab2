using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ChineseZodiac : IntZodiacCalculator
    {
        private static string[] chineseZodiacArray =
        {
            "Мавпа", "Півень", "Собака", "Свиня", "Миша", "Бик", "Тигр", "Кіт", "Дракон", "Змія", "Кінь", "Коза"
        };

        public string getZodiacSign(DateTime date)
        {
            return chineseZodiacArray[date.Year % 12];
        }
    }
}
