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
            get => day;
            set { if (IsValidDate(value, month, year)) day = value; }
        }

        public int Month
        {
            get => month;
            set { if (IsValidDate(day, value, year)) month = value; }
        }

        public int Year
        {
            get => year;
            set { if (IsValidDate(day, month, value)) year = value; }
        }

        public string Day_Of_Week
        {
            get
            {
                int d = day, m = month < 3 ? month + 12 : month, y = month < 3 ? year - 1 : year;
                int h = (d + (13 * (m + 1)) / 5 + y % 100 + (y % 100) / 4 + (y / 100) / 4 - 2 * (y / 100)) % 7;
                return new[] { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" }[(h + 7) % 7];
            }
        }

        public Date() : this(1, 1, 2000) {}

        public Date(int day, int month, int year)
        {
            if (IsValidDate(day, month, year)) (this.day, this.month, this.year) = (day, month, year);
            else (this.day, this.month, this.year) = (1, 1, 2000);
        }

        private bool IsValidDate(int day, int month, int year)
        {
            if (year < 1 || month < 1 || month > 12) return false;
            int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return day >= 1 && day <= daysInMonth[month - 1];
        }

        private bool IsLeapYear(int year) => (year % 400 == 0) || (year % 100 != 0 && year % 4 == 0);

        public int DifferenceInDays(Date other) => Math.Abs(CountDays() - other.CountDays());

        private int CountDays()
        {
            int days = day + (year - 1) * 365 + (year - 1) / 4 - (year - 1) / 100 + (year - 1) / 400;
            int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            for (int m = 0; m < month - 1; m++) days += daysInMonth[m];
            return days;
        }

        public void AddDays(int daysToAdd)
        {
            int totalDays = CountDays() + daysToAdd;
            int newYear = 1;
            while (true)
            {
                int daysInYear = IsLeapYear(newYear) ? 366 : 365;
                if (totalDays > daysInYear) { totalDays -= daysInYear; newYear++; }
                else break;
            }

            int[] daysInMonth = { 31, IsLeapYear(newYear) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int newMonth = 1;
            while (totalDays > daysInMonth[newMonth - 1]) totalDays -= daysInMonth[newMonth++ - 1];

            (day, month, year) = (totalDays, newMonth, newYear);
        }

        public void PrintDate() => WriteLine($"{day:D2}.{month:D2}.{year}");

        public static int operator -(Date d1, Date d2) => d1.DifferenceInDays(d2);
        public static Date operator +(Date d, int days) { Date result = new(d.day, d.month, d.year); result.AddDays(days); return result; }
        public static Date operator ++(Date d) => d + 1;
        public static Date operator --(Date d) => d + (-1);
        public static bool operator >(Date d1, Date d2) => d1.CountDays() > d2.CountDays();
        public static bool operator <(Date d1, Date d2) => d1.CountDays() < d2.CountDays();
        public static bool operator ==(Date d1, Date d2) => d1.day == d2.day && d1.month == d2.month && d1.year == d2.year;
        public static bool operator !=(Date d1, Date d2) => !(d1 == d2);
    }
}
