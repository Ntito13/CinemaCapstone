# Selling Tickets Workflow (10%)

In the workflow sections you must give clear instructions as to how to perform this workflow in your applcation. Use images and diagrams where necessary. Failure to give adequate instructions here may result in loss of marks.

This workflow should do the following things:

- Load information about the cinema without allowing invalid data
- Load information about the schedule of screenings without allowing invalid data
- Select a film screening
- Select a number of tickets to buy
- Select standard or premium tickets
- Select concessions
- Select popcorn and/or soda
- Calculate the total price of the transaction
- Demonstrate the number of tickets available has updated
- Save updated information about screenings


In my application, I created a workflow that lets staff sell tickets and concessions easily while making sure all data stays valid and updated. Here's how to perform the workflow and how I built it.


1. Load Cinema and Screening Data
When the program starts, the cinema and screening info is loaded from files. I added validation to make sure the data is valid.

 // Load cinema details from file
 string cinemaFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\cinema.txt");
 Cinema cinema = CinemaLoader.LoadFromFile(cinemaFile);


2. Select a Film Screening
Staff are shown a list of screenings to choose from. Each screening includes its film title, time, and available seats.

// Sets up a new screening with seat availability
public Screening(Movie movie, string screenId, DateTime startTime, int standardSeats, int premiumSeats)
{
    Movie = movie;
    ScreenId = screenId;
    StartTime = startTime;
    AvailableStandardSeats = standardSeats;
    AvailablePremiumSeats = premiumSeats;
}

3. Select Ticket Quantity
The system asks how many tickets the customer wants. I added a check to make sure they don’t ask for more tickets than are available.

          AvailablePremiumSeats -= quantity;
      }
      else
      {
          if (quantity > AvailableStandardSeats)
              throw new InvalidOperationException("Not enough standard seats available.");

   4. Choose Standard or Premium
After choosing the quantity, they pick the ticket type. Premium tickets are more expensive.

 var ticketOptions = new[] { "Standard", "Premium" };
 int ticketTypeIndex = ConsoleHelpers.GetSelectionFromMenu(ticketOptions, "Choose ticket type:");
 bool isPremium = ticketTypeIndex == 1;


  5. Select Concessions
Customers can choose popcorn and/or soda. If the buyer is a gold member, they automatically get a discount.


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


6. Calculate Total 
The final total is calculated and shown before payment


            // Display final total
            Console.WriteLine($"\nTicket Cost: {(ticketTotalCost / 100.0):C}");
            Console.WriteLine($"Concession Cost: {(totalConcessionCost / 100.0):C}");
            Console.WriteLine($"Total Amount Due: {(grandTotalCost / 100.0):C}");
            Console.WriteLine($"Remaining Seats - Standard: {selectedScreening.AvailableStandardSeats}, Premium: {selectedScreening.AvailablePremiumSeats}");

            _cinema.SaveScreenings("Resources/cinema.txt");

            Console.WriteLine("\nTransaction complete. Press ENTER to return to the main menu...");
            Console.ReadLine();


7. Save and Update Data
Once the transaction is confirmed, the available seats are reduced, and all info is saved


        // Save current screenings to file
        public void SaveScreenings(string path)
        {
            var lines = _screenings.Select(s => s.ToSaveString());
            File.WriteAllLines(path, lines);
        }






