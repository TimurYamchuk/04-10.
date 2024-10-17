using System;

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
                if (!IsValidDate(value, month, year))
                {
                    throw new ArgumentOutOfRangeException("Day", $"Invalid day: {value} for the given month: {month} and year: {year}");
                }
                day = value;
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                if (!IsValidDate(day, value, year))
                {
                    throw new ArgumentOutOfRangeException("Month", $"Invalid month: {value} for the given day: {day} and year: {year}");
                }
                month = value;
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (!IsValidDate(day, month, value))
                {
                    throw new ArgumentOutOfRangeException("Year", $"Invalid year: {value}");
                }
                year = value;
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

                return dayOfWeek switch
                {
                    1 => "Monday",
                    2 => "Tuesday",
                    3 => "Wednesday",
                    4 => "Thursday",
                    5 => "Friday",
                    6 => "Saturday",
                    7 => "Sunday",
                    _ => "Unknown day"
                };
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
            if (!IsValidDate(day, month, year))
            {
                throw new ArgumentOutOfRangeException("Date", $"Invalid date: {day}.{month}.{year}");
            }
            this.day = day;
            this.month = month;
            this.year = year;
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
            Console.WriteLine($"The current date is: {day:D2}.{month:D2}.{year}. Enjoy the moment!");
        }

        public static Date operator +(Date date, int daysToAdd)
        {
            Date newDate = new Date(date.day, date.month, date.year);
            newDate.AddDays(daysToAdd);
            return newDate;
        }

        public static Date operator -(Date date, int daysToSubtract)
        {
            return date + (-daysToSubtract);
        }

        public static bool operator >(Date date1, Date date2)
        {
            return date1.CountDays(date1) > date2.CountDays(date2);
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1.CountDays(date1) < date2.CountDays(date2);
        }

        public static bool operator ==(Date date1, Date date2)
        {
            return date1.day == date2.day && date1.month == date2.month && date1.year == date2.year;
        }

        public static bool operator !=(Date date1, Date date2)
        {
            return !(date1 == date2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Date date)
            {
                return this == date;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (day, month, year).GetHashCode();
        }
    }
}
