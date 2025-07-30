# Creating a Loyalty Scheme (10%)

In the workflow sections you must give clear instructions as to how to perform this workflow in your applcation. Use images and diagrams where necessary. Failure to give adequate instructions here may result in loss of marks.

This workflow should do the following things:
- Load information about members
- Any worker can add a member by providing a first name, surname and email address
- When a member visits 10 times their next standard ticket is free



To register and manage loyalty members, I first load existing members from a file using the MemberLoader class. These members are stored as LoyaltyMembers, which contain the member’s first name, last name, email, and the number of times they’ve visited the cinema.


How to Add a Member:
Any staff member can register a new customer by selecting “Add New Loyalty Member” from the menu. This opens the RegisterMember class, which asks for their details

    var parts = ParseLine(line);

    string firstName = parts["FirstName"];
    string lastName = parts["LastName"];
    string email = parts["Email"];
    int visits = int.Parse(parts["Visits"]);

    LoyaltyMember member = new(firstName, lastName, email, visits);
    members.Add(member);


Each time a loyalty member visits and buys a ticket, their VisitCount goes up. This is tracked in the LoyaltyMember class. Once the member has visited 10 times, the system gives them their next standard ticket for free. Which is checked during the ticket purchasing process.