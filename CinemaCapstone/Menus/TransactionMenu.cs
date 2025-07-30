using Capstone.main_classes;
using Capstone.loaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Menus
{
    internal class TransactionMenu : MenuItem
    {
        private readonly Cinema _cinema;
        private readonly List<LoyaltyMember> _members;
        private readonly string _memberFilePath;

        public TransactionMenu(Cinema cinema, List<LoyaltyMember> members, string memberFilePath)
        {
            _cinema = cinema;
            _members = members;
            _memberFilePath = memberFilePath;
        }

 
        public override string MenuText()
        {
            return "Sell Tickets and Concessions";
        }

        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Choose Customer:");

            // List all members, marking gold members, plus a guest option
            var customerOptions = _members
                .Select(member => $"{member.FullName}{(member.IsGold ? " (Gold)" : "")}")
                .ToList();
            customerOptions.Add("Guest (not a member)");

            int selection = ConsoleHelpers.GetSelectionFromMenu(customerOptions, "Select a member or continue as guest:");
            LoyaltyMember selectedMember = null;

            if (selection < _members.Count)
            {
                selectedMember = _members[selection];
                selectedMember.VisitCount++;

                // If it's their 10th visit, they get a free standard ticket
                if (selectedMember.VisitCount == 10)
                {
                    Console.WriteLine($"{selectedMember.FullName} has earned a FREE standard ticket this time!");
                }

                // Reset visit count if it exceeds 10
                if (selectedMember.VisitCount > 10)
                {
                    selectedMember.VisitCount = 1;
                }

                MemberLoader.SaveMembers(_memberFilePath, _members);
            }

            var transaction = new Transaction(_cinema, selectedMember);
            transaction.DisplayTransactionMenu();

            Console.WriteLine("Press ENTER to return to the main menu...");
            Console.ReadLine();
        }
    }
}
