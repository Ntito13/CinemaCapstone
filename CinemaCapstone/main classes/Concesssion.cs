using System;

namespace Capstone.main_classes
{
    internal class Concession
    {
        public string Name { get; }
        public int Price { get; }

        // Create a concession item
        public Concession(string name, int price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (price <= 0)
                throw new ArgumentException("Price must be more than zero.");

            Name = name;
            Price = price;
        }

        // Display item name and price
        public override string ToString()
        {
            return $"{Name} - {(Price / 100.0):C}";
        }
    }
}
