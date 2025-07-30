# User Interaction (10%)

You should use a reusable, object oriented menu system to manage user interaction. It should not be possible to crash the program with invalid or unexpected user input.


To manage user interaction, I created a reusable, object-oriented menu system. The core of it is a class called MenuItem, which every individual menu option inherits from. This means each menu item can define its own behavior while still being handled in the same way by the program

namespace Capstone.Menus
{
    internal abstract class MenuItem
    {
        public abstract void Select();

        public abstract string MenuText();
    }
}


By designing it like this, the menu system is easy to expand or change. For example, if I want to add a new feature, I just create a new class that extends MenuItem and define what happens when it's selected.

I also made sure the program doesn’t crash if the user inputs something unexpected. Every menu checks for valid input, so if someone types a letter when a number is expected, the program will show an error and let them try again. This keeps everything smooth
