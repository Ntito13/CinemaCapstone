# Self commenting code and explicit comments (5%)

Describe how you have documented your code in terms of naming conventions for member variables and member methods, explicit comments (that add value and not clutter) and summary comments.



In this section of the code, I followed good documentation practices by giving all variables and methods clear names for example, PromptForValidName, and _memberList. These names make the purpose of each part of the code easy to understand

I also used explicit inline comments that explain logic where necessary, for example:

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

I used comments to section the code and to act like a summary so it would be easy to understand how that particular section functions