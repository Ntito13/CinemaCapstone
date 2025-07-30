# Abstraction (10%)

Here you should describe how you have used Inheritance for Code Reuse in your solution.

You should use class diagrams and code snippets where appropriate.


I used abstraction to simplify how menu items are created and used across the whole system. Instead of repeating the same code structure every time I added a new menu option, I created the MenuItem. This class acts like a blueprint which defines what methods all menu items should have.

```cs
namespace Capstone.Menus
{
    internal abstract class MenuItem
    {
        public abstract void Select();

        public abstract string MenuText();
    }
}


