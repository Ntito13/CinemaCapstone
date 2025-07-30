# Encapsulation and Cohesion (10%)

Here you should describe how you have used Encapsulation and Cohesion in your solution.

You should use class diagrams and code snippets where appropriate.


 I used encapsulation to keep my code clean and under control. For example, in the LoyaltyMember class, things like the first name, last name, and email are set once when the member is created — and they can’t be changed later. This helps protect the data from being accidentally changed somewhere else.

 // Member's personal details
 public string FirstName { get; }
 public string LastName { get; }
 public string Email { get; }


Then i made use of cohesion by making sure each class has one clear job. For example, the RegisterMember class is only responsible for handling the logic of registering a new member. It contains methods like PromptForValidName and PromptForValidEmail, which are only related to registering someone. This makes it easier to manage, test, and update the code.


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
