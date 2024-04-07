using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Person
    {
        public string PersonName = "Anonymous";
        public double PersonCurrency = 10.00;

        public List<Item> Inventory = new List<Item>();

        public string GetInventoryItemList()
        {
            string output = "Your Inventory:\n\n";
            foreach (Item item in Inventory)
            {
                item.CheckMeasurementPlurality();
                output += $"* {item.GetItemDescription()}";
            }
            return output;
        }
    }
}
