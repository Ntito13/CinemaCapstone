using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Capstone.main_classes
{
    internal class Cinema
    {
        // Basic details
        public string Name { get; }
        public int StandardTicketPrice { get; set; }
        public int PremiumTicketPrice { get; set; }

        // Internal storage
        private readonly List<Staff> _staff = new();
        private readonly List<Movie> _movies = new();
        private readonly List<Screen> _screens = new();
        private readonly List<Concession> _concessions = new();
        private readonly List<Screening> _screenings = new();

        // Set cinema name when creating it
        public Cinema(string name)
        {
            Name = name;
        }

        // Staff
        public void AddStaff(Staff staff) => _staff.Add(staff);
        public List<Staff> GetStaff() => _staff;

        // Movies
        public void AddMovie(Movie movie) => _movies.Add(movie);
        public List<Movie> GetMovies() => _movies;

        // Screens 
        public void AddScreen(Screen screen) => _screens.Add(screen);
        public List<Screen> GetScreens() => _screens;

        // Concessions 
        public void AddConcession(Concession item) => _concessions.Add(item);
        public List<Concession> GetConcessions() => _concessions;

        // Screenings
        public void AddScreening(Screening screening) => _screenings.Add(screening);
        public List<Screening> GetAvailableScreenings() => _screenings;

        // Save current screenings to file
        public void SaveScreenings(string path)
        {
            var lines = _screenings.Select(s => s.ToSaveString());
            File.WriteAllLines(path, lines);
        }
    }
}

