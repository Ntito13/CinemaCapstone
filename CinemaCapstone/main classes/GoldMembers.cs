using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
