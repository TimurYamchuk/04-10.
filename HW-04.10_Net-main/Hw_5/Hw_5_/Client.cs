using System;
using static System.Console;
using Hw_5_CustomDate;

class Hw_5_Client
{
    static void Main()
    {
        try
        {
            Date date1 = new Date();
            WriteLine("Hi! Let's start by checking the default date:");
            date1.PrintDate();
            WriteLine($"The day of the week is: {date1.Day_Of_Week}");

            WriteLine("\nNow, let's create a custom date. Please enter the day, month, and year one by one:");
            Write("Day: ");
            int day = int.Parse(ReadLine());
            Write("Month: ");
            int month = int.Parse(ReadLine());
            Write("Year: ");
            int year = int.Parse(ReadLine());

            Date date2 = new Date(day, month, year);
            WriteLine("Here’s the date you entered:");
            date2.PrintDate();
            WriteLine($"The day of the week is: {date2.Day_Of_Week}");

            int difference = date1 - date2;
            WriteLine($"\nThe difference between the default date and the one you entered is {difference} days.");

            WriteLine("\nHow many days would you like to add to this date?");
            int daysToAdd = int.Parse(ReadLine());
            date2 = date2 + daysToAdd; 
            WriteLine($"After adding {daysToAdd} days, the new date is:");
            date2.PrintDate();
            WriteLine($"The new day of the week is: {date2.Day_Of_Week}");

            WriteLine("\nLet's add one more day using the '++' operator.");
            date2++;
            date2.PrintDate();
            WriteLine($"The day of the week after incrementing is: {date2.Day_Of_Week}");

            WriteLine("\nNow, let's subtract one day using the '--' operator.");
            date2--;
            date2.PrintDate();
            WriteLine($"The day of the week after decrementing is: {date2.Day_Of_Week}");

            WriteLine("\nLet's compare a few dates.");
            Date date3 = new Date(15, 8, 2025);
            WriteLine("Here’s the default date:");
            date1.PrintDate();
            WriteLine("Here’s the second date (after all operations):");
            date2.PrintDate();
            WriteLine("And here’s a third date (15.08.2025):");
            date3.PrintDate();

            if (date1 > date2)
            {
                WriteLine("The default date is later than the second date.");
            }
            else
            {
                WriteLine("The default date is earlier than or equal to the second date.");
            }

            if (date1 < date3)
            {
                WriteLine("The default date is earlier than the third date.");
            }

            if (date1 == date2)
            {
                WriteLine("The default date and the second date are identical.");
            }
            else
            {
                WriteLine("The default date and the second date are not identical.");
            }

            if (date1 != date2)
            {
                WriteLine("The default date is different from the second date.");
            }
            else
            {
                WriteLine("The default date matches the second date.");
            }
        }
        catch (FormatException)
        {
            WriteLine("Oops! It seems like you entered a non-numeric value. Please enter a valid number for the day, month, and year.");
        }
        catch (Exception ex)
        {
            WriteLine($"Something went wrong: {ex.Message}");
        }
    }
}
