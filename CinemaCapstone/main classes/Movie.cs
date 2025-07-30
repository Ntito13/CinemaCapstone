using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.main_classes
{
    internal class Movie
    {
        // Movie details
        public string Title { get; }
        public int LengthMinutes { get; }
        public string Rating { get; }
        public int RequiredAge { get; }

        // Sets up a new movie, calculating age restrictions from the rating
        public Movie(string title, int lengthMinutes, string rating)
        {
            Title = title;
            LengthMinutes = lengthMinutes;
            Rating = rating;

            // age restriction 
            RequiredAge = rating switch
            {
                "U" => 0,     
                "12" => 12,   
                "15" => 15,   
                "18" => 18,   
                _ => 0        
            };
        }
    }
}
