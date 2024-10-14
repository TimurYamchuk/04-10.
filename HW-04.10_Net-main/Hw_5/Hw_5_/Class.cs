using System;
using static System.Console;
using Hw_5_CustomDate;

class Hw_5_Client
{
    static void Main()
    {
        try
        {
            Date defaultDate = new Date();
            WriteLine("Дата по умолчанию:");
            PrintDateInfo(defaultDate);

            WriteLine("\nВведите дату (день месяц год) по отдельности:");
            int day = int.Parse(ReadLine());
            int month = int.Parse(ReadLine());
            int year = int.Parse(ReadLine());

            Date userDate = new Date(day, month, year);
            WriteLine("Введенная дата:");
            PrintDateInfo(userDate);

            int difference = defaultDate - userDate;
            WriteLine($"\nРазница между датами в днях: {difference}");

            WriteLine("\nВведите количество дней для изменения даты:");
            int daysToAdd = int.Parse(ReadLine());
            userDate += daysToAdd; 
            WriteLine($"Новая дата после добавления {daysToAdd} дней:");
            PrintDateInfo(userDate);

            WriteLine("\nДобавляем один день с помощью оператора '++':");
            userDate++;
            PrintDateInfo(userDate);

            WriteLine("\nВычитаем один день с помощью оператора '--':");
            userDate--;
            PrintDateInfo(userDate);

            WriteLine("\nСравнение дат:");
            Date comparisonDate = new Date(15, 8, 2025);
            CompareDates(defaultDate, userDate, comparisonDate);
        }
        catch (Exception ex)
        {
            WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void PrintDateInfo(Date date)
    {
        date.PrintDate();
        WriteLine($"День недели: {date.Day_Of_Week}");
    }

    static void CompareDates(Date date1, Date date2, Date date3)
    {
        WriteLine("Дата 1:");
        date1.PrintDate();
        WriteLine("Дата 2:");
        date2.PrintDate();
        WriteLine("Дата 3:");
        date3.PrintDate();

        WriteLine(date1 > date2 ? "Дата 1 больше Даты 2" : "Дата 1 меньше или равна Дате 2");
        WriteLine(date1 < date3 ? "Дата 1 меньше Даты 3" : "Дата 1 больше или равна Дате 3");
        WriteLine(date1 == date2 ? "Дата 1 идентична Дате 2" : "Дата 1 не идентична Дате 2");
        WriteLine(date1 != date3 ? "Дата 1 не равна Дате 3" : "Дата 1 равна Дате 3");
    }
}
