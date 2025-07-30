using Capstone.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.main_classes
{
    // Manages the purchase of tickets and concessions
    internal class Transaction
    {
        private readonly Cinema _cinema;
        private readonly LoyaltyMember _currentMember;

        public Transaction(Cinema cinema, LoyaltyMember currentMember)
        {
            _cinema = cinema;
            _currentMember = currentMember;
        }

        // Runs the full ticket and concession workflow
        public void DisplayTransactionMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Ticket and Concession Menu!");

            var screenings = _cinema.GetAvailableScreenings();

            if (screenings.Count == 0)
            {
                Console.WriteLine("No screenings are currently available.");
                return;
            }

            // Choose a screening
            int selectedScreeningIndex = ConsoleHelpers.GetSelectionFromMenu(
                screenings.Select(s => s.Description), "Please select a screening:");
            var selectedScreening = screenings[selectedScreeningIndex];

            // Ticket type selection
            var ticketOptions = new[] { "Standard", "Premium" };
            int ticketTypeIndex = ConsoleHelpers.GetSelectionFromMenu(ticketOptions, "Choose ticket type:");
            bool isPremium = ticketTypeIndex == 1;

            // Checks seat availability
            int seatsAvailable = isPremium ? selectedScreening.AvailablePremiumSeats : selectedScreening.AvailableStandardSeats;

            if (seatsAvailable == 0)
            {
                Console.WriteLine("Sorry, no seats available for the selected type.");
                return;
            }

            // Asks for number of tickets
            int ticketQuantity = ConsoleHelpers.GetIntegerInRange(1, seatsAvailable, "Number of tickets:");

            // Validate age for each ticket holder
            for (int i = 0; i < ticketQuantity; i++)
            {
                int age = ConsoleHelpers.GetIntegerInRange(1, 120, $"Enter age of ticket holder #{i + 1}:");

                if (age < selectedScreening.Movie.RequiredAge)
                {
                    Console.WriteLine("Age restriction not met. Transaction cancelled.");
                    return;
                }
            }

            // Reserve seats
            selectedScreening.ReserveSeats(isPremium, ticketQuantity);

            var selectedItems = new List<Concession>();
            var concessionOptions = _cinema.GetConcessions();
            bool continueAdding = true;

            while (continueAdding)
            {
                Console.Clear();
                Console.WriteLine("Choose your concessions (or select 'Finish'):\n");

                var menu = concessionOptions.Select(c => c.ToString()).ToList();
                menu.Add("Finish");

                int choice = ConsoleHelpers.GetSelectionFromMenu(menu, "Pick an item or finish:");

                if (choice == concessionOptions.Count)
                {
                    continueAdding = false;
                }
                else
                {
                    var chosenItem = concessionOptions[choice];
                    selectedItems.Add(chosenItem);

                    Console.WriteLine($"{chosenItem.Name} added.");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            // Concession and cost calculation
            Console.WriteLine("\nConcessions Selected:");
            var groupedItems = selectedItems.GroupBy(item => item.Name);
            int totalConcessionCost = 0;

            foreach (var group in groupedItems)
            {
                int count = group.Count();
                int originalPrice = group.First().Price;
                int adjustedPrice = originalPrice;

                // Apply 25% discount for gold members
                if (_currentMember?.IsGold == true)
                {
                    adjustedPrice = (int)Math.Round(originalPrice * 0.75);
                }

                int cost = adjustedPrice * count;
                totalConcessionCost += cost;

                Console.WriteLine($"- {group.Key} x{count} @ {(adjustedPrice / 100.0):C} = {(cost / 100.0):C}");
            }

            if (_currentMember?.IsGold == true)
            {
                Console.WriteLine("Gold Membership Discount: 25% off concessions applied.");
            }

            // Calculate ticket total
            int ticketCost = isPremium ? _cinema.PremiumTicketPrice : _cinema.StandardTicketPrice;
            int freeTicketCount = 0;

            // Loyalty bonus: one free standard ticket after 10 visits
            if (_currentMember != null && _currentMember.VisitCount == 10 && !isPremium)
            {
                freeTicketCount = 1;
                Console.WriteLine("Loyalty Bonus: 1 standard ticket is free this visit!");
            }

            int ticketTotalCost = ticketCost * (ticketQuantity - freeTicketCount);
            int grandTotalCost = ticketTotalCost + totalConcessionCost;

            // Display final total
            Console.WriteLine($"\nTicket Cost: {(ticketTotalCost / 100.0):C}");
            Console.WriteLine($"Concession Cost: {(totalConcessionCost / 100.0):C}");
            Console.WriteLine($"Total Amount Due: {(grandTotalCost / 100.0):C}");
            Console.WriteLine($"Remaining Seats - Standard: {selectedScreening.AvailableStandardSeats}, Premium: {selectedScreening.AvailablePremiumSeats}");

            _cinema.SaveScreenings("Resources/cinema.txt");

            Console.WriteLine("\nTransaction complete. Press ENTER to return to the main menu...");
            Console.ReadLine();
        }
    }
}

