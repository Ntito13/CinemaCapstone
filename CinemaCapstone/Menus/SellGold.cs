using Capstone.main_classes;
using Capstone.loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Capstone.Menus
{
    // Lets staff to upgrade a loyalty member to a gold membership
    internal class SellGoldMemb : MenuItem
    {
        private readonly List<LoyaltyMember> _members;
        private readonly string _memberFilePath;

        public SellGoldMemb(List<LoyaltyMember> members, string memberFilePath)
        {
            _members = members;
            _memberFilePath = memberFilePath;
        }

        public override string MenuText()
        {
            return "Sell Gold Membership";
        }

        // this upgrades a standard loyalty member to gold
        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Gold Membership Upgrade");

            var nonGoldMembers = _members.Where(member => !member.IsGold).ToList();

            if (nonGoldMembers.Count == 0)
            {
                Console.WriteLine("All loyalty members are already Gold Members.");
                Console.ReadLine();
                return;
            }

            var names = nonGoldMembers.Select(member => member.FullName).ToList();
            int selectedIndex = ConsoleHelpers.GetSelectionFromMenu(names, "Choose a member to upgrade:");
            var selectedMember = nonGoldMembers[selectedIndex];

            _members.Remove(selectedMember);

            var goldVersion = new GoldMember(
                selectedMember.FirstName,
                selectedMember.LastName,
                selectedMember.Email,
                selectedMember.VisitCount,
                DateTime.Now.AddYears(1) // Set expiry for one year from today
            );

            _members.Add(goldVersion);

            // Save the updated member list to file
            MemberLoader.SaveMembers(_memberFilePath, _members);

            Console.WriteLine($"{selectedMember.FullName} is now a Gold Member!");
            Console.WriteLine($"Membership valid until: {goldVersion.ExpiryDate:dd-MM-yyyy}");
            Console.ReadLine();
        }
    }
}
