using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.main_classes
{
    internal class Screen
    {
        // Unique ID of the screen
        public string Id { get; }

        // Number of standard seats in the screen
        public int StandardSeats { get; }

        // Number of premium seats in the screen
        public int PremiumSeats { get; }

        // Constructor to set up the screen with its ID and seat counts
        public Screen(string id, int standardSeats, int premiumSeats)
        {
            Id = id;
            StandardSeats = standardSeats;
            PremiumSeats = premiumSeats;
        }
    }
}
