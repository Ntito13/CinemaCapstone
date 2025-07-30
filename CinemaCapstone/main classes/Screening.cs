using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.main_classes
{
    internal class Screening
    {
        public Movie Movie { get; }

        public string ScreenId { get; }

        public DateTime StartTime { get; }

        public int AvailableStandardSeats { get; private set; }

        public int AvailablePremiumSeats { get; private set; }

        // Sets up a new screening with seat availability
        public Screening(Movie movie, string screenId, DateTime startTime, int standardSeats, int premiumSeats)
        {
            Movie = movie;
            ScreenId = screenId;
            StartTime = startTime;
            AvailableStandardSeats = standardSeats;
            AvailablePremiumSeats = premiumSeats;
        }

        // Reserves a number of seats (standard or premium)
        public void ReserveSeats(bool isPremium, int quantity)
        {
            if (isPremium)
            {
                if (quantity > AvailablePremiumSeats)
                    throw new InvalidOperationException("Not enough premium seats available.");

                AvailablePremiumSeats -= quantity;
            }
            else
            {
                if (quantity > AvailableStandardSeats)
                    throw new InvalidOperationException("Not enough standard seats available.");

                AvailableStandardSeats -= quantity;
            }
        }

        public string Description => $"{Movie.Title} ({Movie.Rating}) - {StartTime:dd/MM/yyyy HH:mm} on Screen {ScreenId}";

        public string ToSaveString()
        {
            return $"[Screening:{ScreenId}%Movie:{Movie.Title}%Start:{StartTime:yyyyMMddHHmm}%StandardSeats:{AvailableStandardSeats}%PremiumSeats:{AvailablePremiumSeats}]";
        }
    }
}