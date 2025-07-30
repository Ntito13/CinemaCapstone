using System;
using System.Collections.Generic;
using Capstone.main_classes;

namespace Capstone.Menus
{
    //  interface for staff to interact with the cinema system
    internal class CinemaConsoleMenu : ConsoleMenu
    {
        private readonly Cinema _cinema;                
        private readonly Staff _loggedInStaff;          
        private readonly List<LoyaltyMember> _members;   
        private readonly string _memberFilePath;        

        public CinemaConsoleMenu(Cinema cinema, Staff loggedInStaff, List<LoyaltyMember> members, string memberFilePath)
        {
            _cinema = cinema;
            _loggedInStaff = loggedInStaff;
            _members = members;
            _memberFilePath = memberFilePath;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear(); 

            // Add common menu actions
            _menuItems.Add(new TransactionMenu(_cinema, _members, _memberFilePath));
            _menuItems.Add(new ViewScreenings(_cinema));
            _menuItems.Add(new RegisterMember(_members, _memberFilePath));
            _menuItems.Add(new SellGoldMemb(_members, _memberFilePath));

            // Provides access to scheduling tools only for managers
            if (_loggedInStaff.IsManager)
            {
                _menuItems.Add(new ScheduleEditor(_cinema));
            }

            _menuItems.Add(new ExitMenuItem(this));
        }

        // Displays welcome message and current staff information
        public override string MenuText()
        {
            return $" Welcome to Hullywood!\n" +
                   $"Cinema: {_cinema.Name}\n" +
                   $"Logged in as: {_loggedInStaff.FullName} ({_loggedInStaff.JobTitle})\n";
        }
    }
}
