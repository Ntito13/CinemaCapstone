using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Capstone.Menus;
using Capstone.loaders;

namespace Capstone.main_classes
{
    // handles registration of new loyalty members
    internal class RegisterMember : MenuItem
    {
        private readonly List<LoyaltyMember> _memberList;
        private readonly string _savePath;

        public RegisterMember(List<LoyaltyMember> memberList, string savePath)
        {
            _memberList = memberList;
            _savePath = savePath;
        }

        public override string MenuText()
        {
            return "Add New Loyalty Member";
        }

        // Runs when this menu option is chosen
        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("=== Loyalty Member Registration ===");

            string firstName = PromptForValidName("First Name: ");
            string lastName = PromptForValidName("Last Name: ");
            string email = PromptForValidEmail("Email: ");

            var newMember = new LoyaltyMember(firstName, lastName, email);
            _memberList.Add(newMember);
            MemberLoader.SaveMembers(_savePath, _memberList);

            Console.WriteLine($"\n{newMember.FullName} has been registered!");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Ensures input is a name starting with uppercase and only letters
        private string PromptForValidName(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[A-Z][a-zA-Z]*$"))
                    return input;

                Console.WriteLine("Invalid name. Use only letters, and start with a capital letter.");
            }
        }

        // Checks that the entered email is in proper format
        private string PromptForValidEmail(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? email = Console.ReadLine()?.Trim();

                bool valid = !string.IsNullOrEmpty(email)
                             && Regex.IsMatch(email, @"^(?!.*[.@]{2})[A-Za-z0-9]+([._]?[A-Za-z0-9]+)*@[A-Za-z0-9-]+\.[A-Za-z]{2,}$")
                             && !email.StartsWith(".") && !email.StartsWith("@")
                             && !email.EndsWith(".") && !email.EndsWith("@");

                if (valid)
                    return email;

                Console.WriteLine("Invalid email format. Please enter a valid address.");
            }
        }
    }
}
