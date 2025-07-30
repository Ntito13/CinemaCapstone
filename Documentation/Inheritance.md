# Inheritance for Code Reuse (10%)

Here you should describe how you have used Inheritance for Code Reuse in your solution.

You should use class diagrams and code snippets where appropriate.


To avoid repeating code, I used inheritance. I created an class called MenuItem which defines two methods: Select() and MenuText(). Then I made another class named RegisterNewMembers to inherit from it.

The class gets the basic structure from MenuItem, but they can adds its own custom logic inside the methods.

Menuitem class:

namespace Capstone.Menus
{
    internal abstract class MenuItem
    {
        public abstract void Select();

        public abstract string MenuText();
    }

This is how registerNewmembers uses it:
       
        
        public RegisterMember(List<LoyaltyMember> memberList, string savePath)
        {
            _memberList = memberList;
            _savePath = savePath;
        }

        public override string MenuText()
        {
            return "Add New Loyalty Member";


 This way, all my menu options work the same way behind the scenes, which keeps the program clean.


    
    

