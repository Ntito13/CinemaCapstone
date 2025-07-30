using System;

namespace Capstone.main_classes
{
    // Represents a staff member within the cinema
    internal class Staff
    {
        public string StaffId { get; }

        public string JobTitle { get; }

        public string FirstName { get; }

        public string LastName { get; }

        // initialises a staff member with required details
        public Staff(string staffId, string jobTitle, string firstName, string lastName)
        {
            StaffId = staffId ?? throw new ArgumentNullException(nameof(staffId));
            JobTitle = jobTitle ?? throw new ArgumentNullException(nameof(jobTitle));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));

            if (!JobTitle.Equals("Manager", StringComparison.OrdinalIgnoreCase) &&
                !JobTitle.Equals("General staff", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Job title must be either 'Manager' or 'General staff'.");
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        // Checks if the person is a manager
        public bool IsManager => JobTitle.Equals("Manager", StringComparison.OrdinalIgnoreCase);
    }
}
