# Expanding the Loyalty Scheme to Include Gold Membership (10%)
In the workflow sections you must give clear instructions as to how to perform this workflow in your applcation. Use images and diagrams where necessary. Failure to give adequate instructions here may result in loss of marks.

This workflow should do the following things:
- Load information about members
- Sell an annual membership to turn a loyalty scheme member into a gold member
- Gold members get a 25% discount on all concessions


For Gold Memberships, I extended the regular loyalty system to allow workers to upgrade any standard loyalty member into a Gold Member by selling them an annual membership.

Step 1: Loading Existing Members
When the app starts, it loads all saved loyalty and gold members from file using the MemberLoader class. Each member is stored as either a LoyaltyMember or GoldMember object.

The base class LoyaltyMember contains shared info like name, email, and number of visits, while GoldMember inherits from it and adds specific features, like the 25% discount. This is shown here

namespace Capstone.main_classes
{
    // loyalty member with gold status and an expiry date
    internal class GoldMember : LoyaltyMember
    {
        // Date when the gold membership will expire
        public DateTime ExpiryDate { get; set; }

        // Create a gold member 
        public GoldMember(string firstName, string lastName, string email, int visits, DateTime expiryDate)
            : base(firstName, lastName, email, visits)
        {
            ExpiryDate = expiryDate;
        }

        public override bool IsGold => true;

        public override string ToSaveString()
        {
            return $"[Gold:FirstName:{FirstName}%LastName:{LastName}%Email:{Email}%Visits:{VisitCount}%Expiry:{ExpiryDate:yyyy-MM-dd}]";
        }
    }


 Step 2: Selling Gold Membership
To upgrade someone to gold, staff select the “Upgrade to Gold Membership” option from the menu. This runs the RegisterGoldMember class. It lists current loyalty members, asks the staff to choose which one, and then converts them to a gold member. 

  // Create a gold member 
  public GoldMember(string firstName, string lastName, string email, int visits, DateTime expiryDate)
      : base(firstName, lastName, email, visits)
  
   Once upgraded, the member’s record is saved as a gold member.


   Step 3: Gold Member Discount
Any time a Gold Member buys concessions like popcorn or drinks, they get 25% off the price. The discount is applied during the concession purchase process.

// Apply 25% discount for gold members
    if (_currentMember?.IsGold == true)
    {
        adjustedPrice = (int)Math.Round(originalPrice * 0.75);
    }

    int cost = adjustedPrice * count;
    totalConcessionCost += cost;

    Console.WriteLine($"- {group.Key} x{count} @ {(adjustedPrice / 100.0):C} = {(cost / 100.0):C}");
}

if (_currentMember?.IsGold == true)
{
    Console.WriteLine("Gold Membership Discount: 25% off concessions applied.");
}


