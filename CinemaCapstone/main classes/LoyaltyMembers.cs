using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class LoyaltyMember
{
    // Member's personal details
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    // How many times the member has visited the cinema
    public int VisitCount { get; set; }

    // Set up a new member with visit count
    public LoyaltyMember(string firstName, string lastName, string email, int visits = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitCount = visits;
    }

    public string FullName => $"{FirstName} {LastName}";

    public virtual bool IsGold => false;

    // Save the member's details 
    public virtual string ToSaveString()
    {
        return $"[Loyalty:FirstName:{FirstName}%LastName:{LastName}%Email:{Email}%Visits:{VisitCount}]";
    }
}
