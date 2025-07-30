using Capstone.main_classes;
using System;
using System.Linq;

namespace Capstone.Menus
{
    // This menu item allows a manager to add a new screening to the schedule
    internal class ScheduleEditor(Cinema cinema) : MenuItem
    {
        private readonly Cinema _cinema = cinema;

        public override string MenuText()
        {
            return "Edit Screening Schedule (Manager)";
        }

        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Schedule Editor");

            // Asks which screen to use
            var screens = _cinema.GetScreens();
            var screenOptions = screens.Select(s => $"Screen {s.Id}").ToList();
            int screenChoice = ConsoleHelpers.GetSelectionFromMenu(screenOptions, "Select a screen:");
            var chosenScreen = screens[screenChoice];

            // Asks which movie to show
            var movies = _cinema.GetMovies();
            var movieOptions = movies.Select(m => $"{m.Title} ({m.Rating})").ToList();
            int movieChoice = ConsoleHelpers.GetSelectionFromMenu(movieOptions, "Select a movie:");
            var chosenMovie = movies[movieChoice];

            // Asks for the date and time to show the movie
            Console.Write("Enter start date and time (dd-MM-yyyy HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            {
                Console.WriteLine("Invalid date/time format.");
                Console.ReadLine();
                return;
            }

            // Checks for schedule conflicts or rule violations
            if (!IsScheduleValid(chosenScreen.Id, startTime, chosenMovie.LengthMinutes))
            {
                Console.WriteLine("Schedule Invalid: overlaps with another or breaks trailer/turnaround rules.");
                Console.ReadLine();
                return;
            }

            // Asks how many seats should be reserved
            int standardSeats = ConsoleHelpers.GetIntegerInRange(0, chosenScreen.StandardSeats, "How many standard seats?");
            int premiumSeats = ConsoleHelpers.GetIntegerInRange(0, chosenScreen.PremiumSeats, "How many premium seats?");

            var newScreening = new Screening(chosenMovie, chosenScreen.Id, startTime, standardSeats, premiumSeats);
            _cinema.AddScreening(newScreening);

            Console.WriteLine("Screening added successfully!");
            Console.ReadLine();
        }

        // Checks if the chosen time fits the schedule
        private bool IsScheduleValid(string screenId, DateTime newStart, int movieLength)
        {
            var screenings = _cinema.GetAvailableScreenings()
                .Where(s => s.ScreenId == screenId)
                .OrderBy(s => s.StartTime)
                .ToList();

            DateTime newEnd = newStart
                .AddMinutes(20)
                .AddMinutes(movieLength)
                .AddMinutes(GetTurnaroundMinutes(screenId)); 

            foreach (var s in screenings)
            {
                DateTime existingStart = s.StartTime;
                DateTime existingEnd = s.StartTime
                    .AddMinutes(20)
                    .AddMinutes(s.Movie.LengthMinutes)
                    .AddMinutes(GetTurnaroundMinutes(s.ScreenId));

                if (newStart < existingEnd && newEnd > existingStart)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetTurnaroundMinutes(string screenId)
        {
            var screen = _cinema.GetScreens().First(s => s.Id == screenId);
            int totalSeats = screen.StandardSeats + screen.PremiumSeats;

            if (totalSeats <= 50) return 15;
            if (totalSeats <= 100) return 30;
            return 45;
        }
    }
}
