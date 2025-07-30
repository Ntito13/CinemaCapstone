using Capstone;
using Capstone.main_classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone.loaders
{
    internal static class CinemaLoader
    {
        // Loads cinema details from a text file
        public static Cinema LoadFromFile(string filePath)
        {
            string[] rawLines = File.ReadAllLines(filePath);
            var cinema = new Cinema("Hullywood");

            var movieLookup = new Dictionary<string, Movie>();
            var screenLookup = new Dictionary<string, Screen>();

            foreach (var line in rawLines)
            {
                if (line.StartsWith("[Movie:"))
                {
                    var data = ParseLineParts(line);
                    string title = data["Movie"];
                    int duration = int.Parse(data["Length"]);
                    string rating = data["Rating"];

                    var movie = new Movie(title, duration, rating);
                    movieLookup[title] = movie;
                    cinema.AddMovie(movie);
                }
                else if (line.StartsWith("[Concession:"))
                {
                    var data = ParseLineParts(line);
                    string itemName = data["Concession"];
                    int itemPrice = int.Parse(data["Price"]);

                    var item = new Concession(itemName, itemPrice);
                    cinema.AddConcession(item);
                }
                else if (line.StartsWith("[Screen:"))
                {
                    var data = ParseLineParts(line);
                    string id = data["Screen"];
                    int premium = int.Parse(data["NumPremiumSeat"]);
                    int standard = int.Parse(data["NumStandardSeat"]);

                    var screen = new Screen(id, standard, premium);
                    screenLookup[id] = screen;
                    cinema.AddScreen(screen);
                }
                else if (line.StartsWith("[Screening:"))
                {
                    var data = ParseLineParts(line);
                    string screenId = data["Screening"];
                    string movieName = data["Movie"];
                    DateTime startTime = DateTime.ParseExact(data["Start"], "ddMMyyyyHHmm", null);
                    int stdSeats = int.Parse(data["StandardSeats"]);
                    int prmSeats = int.Parse(data["PremiumSeats"]);

                    if (movieLookup.TryGetValue(movieName, out var foundMovie))
                    {
                        var screening = new Screening(foundMovie, screenId, startTime, stdSeats, prmSeats);
                        cinema.AddScreening(screening);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Movie '{movieName}' not found when creating screening.");
                    }
                }
                else if (line.StartsWith("[Ticket:Standard"))
                {
                    var parts = line.Trim('[', ']').Split('%');
                    cinema.StandardTicketPrice = int.Parse(parts[1]);
                }
                else if (line.StartsWith("[Ticket:Premium"))
                {
                    var parts = line.Trim('[', ']').Split('%');
                    cinema.PremiumTicketPrice = int.Parse(parts[1]);
                }
            }

            return cinema;
        }

        // extracts specific parts of the data
        private static Dictionary<string, string> ParseLineParts(string rawLine)
        {
            var partsDict = new Dictionary<string, string>();
            string cleaned = rawLine.Trim('[', ']');
            var parts = cleaned.Split('%');

            foreach (var part in parts)
            {
                var pair = part.Split(':', 2);
                if (pair.Length == 2)
                {
                    partsDict[pair[0]] = pair[1];
                }
            }

            return partsDict;
        }
    }
}

