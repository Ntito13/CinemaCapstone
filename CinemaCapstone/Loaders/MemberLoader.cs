using Capstone;
using Capstone.main_classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Capstone.loaders
{
    internal static class MemberLoader
    {
        // Load all loyalty and gold members from file
        public static List<LoyaltyMember> LoadMembers(string path)
        {
            List<LoyaltyMember> members = new();

            if (!File.Exists(path))
                return members;

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                try
                {
                    if (line.StartsWith("[Loyalty"))
                    {
                        var parts = ParseLine(line);

                        string firstName = parts["FirstName"];
                        string lastName = parts["LastName"];
                        string email = parts["Email"];
                        int visits = int.Parse(parts["Visits"]);

                        LoyaltyMember member = new(firstName, lastName, email, visits);
                        members.Add(member);
                    }
                    else if (line.StartsWith("[Gold"))
                    {
                        var parts = ParseLine(line);

                        string firstName = parts["FirstName"];
                        string lastName = parts["LastName"];
                        string email = parts["Email"];
                        int visits = int.Parse(parts["Visits"]);
                        DateTime expiry = DateTime.ParseExact(parts["Expiry"], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        GoldMember gold = new(firstName, lastName, email, visits, expiry);
                        members.Add(gold);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Problem reading: {line}");
                    Console.WriteLine($"Reason: {ex.Message}");
                }
            }

            return members;
        }

        // Save all members to file
        public static void SaveMembers(string path, List<LoyaltyMember> members)
        {
            List<string> lines = new();

            foreach (var member in members)
            {
                lines.Add(member.ToSaveString());
            }

            File.WriteAllLines(path, lines);
        }

        private static Dictionary<string, string> ParseLine(string line)
        {
            Dictionary<string, string> parts = new();

            line = line.Trim('[', ']');
            string[] tokens = line.Split('%');

            int start = tokens[0].Contains(":") ? 0 : 1;

            for (int i = start; i < tokens.Length; i++)
            {
                string[] keyValue = tokens[i].Split(':', 2);
                if (keyValue.Length == 2)
                {
                    parts[keyValue[0]] = keyValue[1];
                }
            }

            return parts;
        }
    }
}
