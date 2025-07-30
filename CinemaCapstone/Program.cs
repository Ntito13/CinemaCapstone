using System;
using System.IO;
using System.Linq;
using Capstone.main_classes;
using Capstone.Menus;
using Capstone.loaders;

class Program
{
    static void Main()
    {
        Console.Clear();
        Console.WriteLine(" Welcome to Hullywood Cinema\n");

        // Load cinema details from file
        string cinemaFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\cinema.txt");
        Cinema cinema = CinemaLoader.LoadFromFile(cinemaFile);

        // Load loyalty members
        string membersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "members.txt");
        var members = MemberLoader.LoadMembers(membersFile);

        // Load staff details
        string staffFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\staff.txt");
        var staffList = StaffLoader.LoadStaffFromFile(staffFile);

        // Add staff to the cinema
        foreach (var staff in staffList)
        {
            cinema.AddStaff(staff);
        }

        // Ask user to select a staff account 
        Console.WriteLine("Staff Login:\n");
        var staffOptions = cinema.GetStaff()
                                 .Select(s => $"{s.FullName} ({s.JobTitle})")
                                 .ToList();

        int chosenIndex = ConsoleHelpers.GetSelectionFromMenu(staffOptions, "Choose your account:");
        Staff loggedIn = cinema.GetStaff()[chosenIndex];

        Console.WriteLine($"\nWelcome, {loggedIn.FullName} ({loggedIn.JobTitle})");
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();

        // Open the main menu 
        var menu = new CinemaConsoleMenu(cinema, loggedIn, members, membersFile);
        menu.Run();
    }
}
