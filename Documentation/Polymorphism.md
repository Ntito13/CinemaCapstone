# Polymorphism (10%)

Here you should describe how you have used Polymorphism in your solution.

You should use class diagrams and code snippets where appropriate.



I used polymorphism to make my code more flexible and reusable, especially when building the menu system. I created an class called MenuItem, which acts like a base class for all menu options in the application. This class has two abstract methods: Select() and MenuText(), which are meant to be overridden by every subclass.

So instead of writing different logic to handle every single menu item separately, I created specific classes like RegisterMember, and GoldMember, and each one inherits from MenuItem. These classes all use their own version of Select() and MenuText(), depending on what that option is supposed to do.

Because of polymorphism, I can treat all these different menu items the same way in my program. For example, I can put them all into a list of type List<MenuItem>, and when the user picks one, I just call Select() — and the correct one runs automatically.

