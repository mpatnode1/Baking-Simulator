using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Person
    {
        private string personName = "Anonymous Baker";
        public string PersonName { get { return personName; } set{ personName = value; } } 
        public double PersonCurrency = 10.00;

        public List<Item> Inventory = new List<Item>();

        public List<string> defaultNames = new List<string>() {"Daryl", "Kimmy", "Mike", "Steve-O", "Howard", "Jennifer", "Jessica", "Riley", "Denise", "Cameron", "Robert", "Morgan", "Emma", "Atlas", "Jimmy", "Marcus", "张" };
        public string GetInventoryItemList()
        {
            string output = "Inventory:\n\n";
            foreach (Item item in Inventory)
            {
                item.CheckMeasurementPlurality();
                output += $"* {item.GetItemDescription()}";
            }
            return output;
        }

        public virtual void SetDefaultName()
        {
            this.PersonName += GetRandomName();
        }

        public string GetRandomName()
        {
            string namePicked = defaultNames[new Random().Next(defaultNames.Count)];
            defaultNames.Remove(namePicked);
            return namePicked;
        }
    }
}
