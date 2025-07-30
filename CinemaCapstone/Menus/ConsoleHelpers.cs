using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Menus
{
    internal static class ConsoleHelpers
    {
        // Gets a number between min and max from the user
        public static int GetIntegerInRange(int min, int max, string prompt)
        {
            if (min > max)
            {
                throw new ArgumentException($"Minimum ({min}) cannot be greater than maximum ({max}).");
            }

            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine($"Choose a number from {min} to {max}:");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    if (number >= min && number <= max)
                    {
                        return number;
                    }

                    Console.WriteLine($"That number is out of range. It must be between {min} and {max}.");
                }
                else
                {
                    Console.WriteLine($"'{input}' is not a valid number. Try again.");
                }
            }
        }

        // Displays a menu and returns the users choice
        public static int GetSelectionFromMenu(IEnumerable<string> options, string title)
        {
            var menu = new StringBuilder();
            menu.AppendLine(title);

            int index = 1;
            foreach (var item in options)
            {
                menu.AppendLine($"{index++}. {item}");
            }

            int chosen = GetIntegerInRange(1, index - 1, menu.ToString());
            return chosen - 1;
        }
    }
}
