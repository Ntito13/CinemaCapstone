using Capstone.main_classes;
using System;

namespace Capstone.Menus
{
    internal class ViewScreenings : MenuItem
    {
        private Cinema _cinemaRef;

        public ViewScreenings(Cinema cinema)
        {
            _cinemaRef = cinema;
        }

        public override string MenuText()
        {
            return "See All Screenings";
        }

        public override void Select()
        {
            Console.Clear();
            ShowScreeningList();  // Display all available screenings
            PromptReturn();       
        }

        // Loads and prints all screenings
        private void ShowScreeningList()
        {
            var upcomingScreenings = _cinemaRef.GetAvailableScreenings();

            if (upcomingScreenings == null || upcomingScreenings.Count == 0)
            {
                Console.WriteLine("Sorry, there are no screenings scheduled at the moment.");
                return;
            }

            foreach (var film in upcomingScreenings)
            {
                DisplayScreeningDetails(film);
            }
        }

        // Print out information for a single screening
        private void DisplayScreeningDetails(Screening screening)
        {
            Console.WriteLine($"- {screening.Description}");
            Console.WriteLine($"  Standard Seats: {screening.AvailableStandardSeats}");
            Console.WriteLine($"  Premium Seats : {screening.AvailablePremiumSeats}");
            Console.WriteLine(); 
        }

        private void PromptReturn()
        {
            Console.WriteLine("Press ENTER to return to the main menu.");
            Console.ReadLine();
        }
    }
}
