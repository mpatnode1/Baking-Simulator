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
        public double PersonCurrency = 0;

        public List<Item> Inventory = new List<Item>();        
        
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

        public virtual void SetDefaultName(Game game)
        {
            this.PersonName += GetRandomName(game);
        }

        public string GetRandomName(Game game)
        {
            string namePicked = game.defaultNames[new Random().Next(game.defaultNames.Count)];
            game.defaultNames.Remove(namePicked);
            return namePicked;
        }
    }
}
