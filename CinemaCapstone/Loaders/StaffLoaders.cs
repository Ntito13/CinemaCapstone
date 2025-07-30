using Capstone;
using Capstone.main_classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone.loaders
{
    internal static class StaffLoader
    {
        // Load staff data from file 
        public static List<Staff> LoadStaffFromFile(string path)
        {
            List<Staff> staffList = new();
            HashSet<string> seenIds = new(); // Track used staff IDs

            if (!File.Exists(path))
                throw new FileNotFoundException($"Missing staff file: {path}");

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (!line.StartsWith("[Staff:"))
                    continue;

                var parts = ParseLine(line);

                // Get required values
                if (!parts.TryGetValue("Staff", out string id) ||
                    !parts.TryGetValue("Level", out string role) ||
                    !parts.TryGetValue("FirstName", out string firstName) ||
                    !parts.TryGetValue("LastName", out string lastName))
                {
                    throw new Exception($"Staff entry is incomplete: {line}");
                }

                if (seenIds.Contains(id))
                    throw new Exception($"Duplicate ID: {id}");

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                    throw new Exception($"Missing name: \"{firstName} {lastName}\" in line: {line}");

                if (!role.Equals("Manager", StringComparison.OrdinalIgnoreCase) &&
                    !role.Equals("General staff", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Invalid role: \"{role}\" in line: {line}");
                }

                // All checks passed — add staff to list
                seenIds.Add(id);
                staffList.Add(new Staff(id, role, firstName, lastName));
            }

            return staffList;
        }

        private static Dictionary<string, string> ParseLine(string line)
        {
            Dictionary<string, string> parts = new();

            line = line.Trim('[', ']');
            string[] tokens = line.Split('%');

            foreach (string token in tokens)
            {
                string[] keyValue = token.Split(':', 2);
                if (keyValue.Length == 2)
                {
                    parts[keyValue[0]] = keyValue[1];
                }
            }

            return parts;
        }
    }
}
