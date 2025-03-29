using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class WesternZodiac : IntZodiacCalculator
    {

        public string getZodiacSign(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            switch (month)
            {
                case 1:
                    if (day <= 20) return "Козеріг";
                    return "Водолій";
                case 2:
                    if (day <= 19) return "Водолій";
                    return "Риби";
                case 3:
                    if (day <= 20) return "Риби";
                    return "Овен";
                case 4:
                    if (day <= 20) return "Овен";
                    return "Телець";
                case 5:
                    if (day <= 21) return "Телець";
                    return "Близнюки";
                case 6:
                    if (day <= 21) return "Близнюки";
                    return "Рак";
                case 7:
                    if (day <= 22) return "Рак";
                    return "Лев";
                case 8:
                    if (day <= 23) return "Лев";
                    return "Діва";
                case 9:
                    if (day <= 23) return "Діва";
                    return "Терези";
                case 10:
                    if (day <= 23) return "Терези";
                    return "Скорпіон";
                case 11:
                    if (day <= 22) return "Скорпіон";
                    return "Стрілець";
                case 12:
                    if (day <= 21) return "Стрілець";
                    return "Козеріг";
            }

            return "Невідомий знак зодіаку";
        }
    }
}
