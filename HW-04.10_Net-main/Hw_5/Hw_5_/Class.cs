using System;
using static System.Console;

namespace Hw_5_CustomDate
{
    class Date
    {
        private int day;
        private int month;
        private int year;

        public int Day
        {
            get { return day; }
            set
            {
                if (IsValidDate(value, month, year))
                    day = value;
                else
                    WriteLine("Недопустимое значение для дня.");
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                if (IsValidDate(day, value, year))
                    month = value;
                else
                    WriteLine("Недопустимое значение для месяца.");
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (IsValidDate(day, month, value))
                    year = value;
                else
                    WriteLine("Недопустимое значение для года.");
            }
        }

        public string Day_Of_Week
        {
            get
            {
                int d = day;
                int m = month;
                int y = year;

                if (m < 3)
                {
                    m += 12;
                    y -= 1;
                }

                int K = y % 100;
                int J = y / 100;
                int h = (d + (13 * (m + 1)) / 5 + K + (K / 4) + (J / 4) + 5 * J) % 7;
                int dayOfWeek = ((h + 5) % 7) + 1;

                switch (dayOfWeek)
                {
                    case 1: return "Понедельник";
                    case 2: return "Вторник";
                    case 3: return "Среда";
                    case 4: return "Четверг";
                    case 5: return "Пятница";
                    case 6: return "Суббота";
                    case 7: return "Воскресенье";
                    default: return "Неизвестно";
                }
            }
        }

        public Date()
        {
            day = 1;
            month = 1;
            year = 2000;
        }

        public Date(int day, int month, int year)
        {
            if (IsValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            else
            {
                WriteLine("Недопустимая дата. Установлена дата по умолчанию.");
                this.day = 1;
                this.month = 1;
                this.year = 2000;
            }
        }

        private bool IsValidDate(int day, int month, int year)
        {
            if (year < 1 || month < 1 || month > 12)
                return false;

            int[] daysInMonth =
            {
                31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };

            if (day < 1 || day > daysInMonth[month - 1])
                return false;

            return true;
        }

        private bool IsLeapYear(int year)
        {
            if (year % 400 == 0)
                return true;
            else if (year % 100 == 0)
                return false;
            else if (year % 4 == 0)
                return true;
            else
                return false;
        }

        public int DifferenceInDays(Date other)
        {
            int totalDays1 = CountDays(this);
            int totalDays2 = CountDays(other);

            return Math.Abs(totalDays1 - totalDays2);
        }

        private int CountDays(Date date)
        {
            int days = date.day;

            for (int y = 1; y < date.year; y++)
            {
                days += IsLeapYear(y) ? 366 : 365;
            }

            int[] daysInMonth =
            {
                31, IsLeapYear(date.year) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };

            for (int m = 1; m < date.month; m++)
            {
                days += daysInMonth[m - 1];
            }

            return days;
        }

        public void AddDays(int daysToAdd)
        {
            int totalDays = CountDays(this) + daysToAdd;

            int newYear = 1;
            while (true)
            {
                int daysInYear = IsLeapYear(newYear) ? 366 : 365;
                if (totalDays > daysInYear)
                {
                    totalDays -= daysInYear;
                    newYear++;
                }
                else
                {
                    break;
                }
            }

            int[] daysInMonth =
            {
                31, IsLeapYear(newYear) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };
            int newMonth = 1;
            while (true)
            {
                if (totalDays > daysInMonth[newMonth - 1])
                {
                    totalDays -= daysInMonth[newMonth - 1];
                    newMonth++;
                }
                else
                {
                    break;
                }
            }

            day = totalDays;
            month = newMonth;
            year = newYear;
        }

        public void PrintDate()
        {
            WriteLine($"{day:D2}.{month:D2}.{year}");
        }

        // Перегрузка оператора "-"
        public static int operator -(Date d1, Date d2)
        {
            return d1.DifferenceInDays(d2);
        }

        // Перегрузка оператора "+"
        public static Date operator +(Date d, int days)
        {
            Date result = new(d.day, d.month, d.year);
            result.AddDays(days);
            return result;
        }

        // Перегрузка оператора "++"
        public static Date operator ++(Date d)
        {
            return d + 1;
        }

        // Перегрузка оператора "--"
        public static Date operator --(Date d)
        {
            return d + (-1);
        }

        // Перегрузка оператора ">"
        public static bool operator >(Date d1, Date d2)
        {
            return d1.DifferenceInDays(d2) > 0;
        }

        // Перегрузка оператора "<"
        public static bool operator <(Date d1, Date d2)
        {
            return d1.DifferenceInDays(d2) < 0;
        }

        // Перегрузка оператора "=="
        public static bool operator ==(Date d1, Date d2)
        {
            return (d1.day == d2.day && d1.month == d2.month && d1.year == d2.year);
        }

        // Перегрузка оператора "!="
        public static bool operator !=(Date d1, Date d2)
        {
            return !(d1 == d2);
        }
    }
}