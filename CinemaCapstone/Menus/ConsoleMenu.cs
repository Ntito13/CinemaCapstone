using System;
using System.Collections.Generic;

namespace Capstone.Menus
{
    internal abstract class ConsoleMenu
    {
        // Stores all the options shown in the menu
        protected List<MenuItem> _menuItems = new List<MenuItem>();

        public bool IsActive { get; set; } = true;

        public abstract void CreateMenu();     
        public abstract string MenuText();     

        // Launches the menu and loops until user exits
        public void Run()
        {
            IsActive = true;

            while (IsActive)
            {
                Console.Clear();
                Console.WriteLine(MenuText());     
                CreateMenu();                     

                var optionTexts = new List<string>();
                foreach (var item in _menuItems)
                {
                    optionTexts.Add(item.MenuText());   
                }

                int selectedIndex = ConsoleHelpers.GetSelectionFromMenu(optionTexts, "Choose an option:");
                _menuItems[selectedIndex].Select();     
            }
        }
    }
}
