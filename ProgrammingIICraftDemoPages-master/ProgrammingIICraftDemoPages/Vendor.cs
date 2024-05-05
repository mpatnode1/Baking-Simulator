using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Vendor : Person
    {
        private int shopInteractionCount = 0;
        public int ShopInteractionCount { get { return shopInteractionCount; } set { shopInteractionCount = value; } }
   
        public override void SetDefaultName(Game game)
        {

            this.PersonName = "ShopKeep ";
            base.SetDefaultName(game);
        }

        public string VendorIntroduction(Game game)
        {

            string output = "";
            if (shopInteractionCount == 0)
            {
                output += $"Welcome to my shop {game.Player.PersonName}. My name is {PersonName}.\n";
                
                //TO DO when buy button is complete move shop interaction ++ to there
            }
            else
            {
                output += $"Would you like to buy anything else, {game.Player.PersonName}?\n";
                output += $"These are the wares I have left:\n";
            }
            return output;
        }

        public void RestockVendor(Game game)
        {
            Random rnd = new Random();

            foreach (Item item in game.AllActiveItemsInGame)
            {
                int RandomItemAmount = rnd.Next(5);

                //if vendor already has item in inventory it will increase their item amount by random number < 4
                if (Inventory.Any(x => x.ItemName == item.ItemName))
                {
                    Item? itemInVendorInventory = Inventory.Find(x => x.ItemName == item.ItemName);
                    if (itemInVendorInventory != null)
                    {
                        //stops vendor from restocking items excessively/infinitely
                        if (itemInVendorInventory.ItemAmount < 12)
                        {
                            itemInVendorInventory.ItemAmount += RandomItemAmount;
                        }
                    }

                }
                //if vendor is out of stock of item it will add a new item (clone) to inventory then increase item amount
                //unless random number was 0
                else
                {
                    if (RandomItemAmount > 0)
                    {
                        Item cloneForVendorInventory = item.GetMemberwiseClone();
                        Inventory.Add(cloneForVendorInventory);

                        Item? itemInVendorInventory = Inventory.Find(x => x.ItemName == item.ItemName);
                        if (itemInVendorInventory != null)
                        {
                            itemInVendorInventory.ItemAmount += RandomItemAmount;
                        }
                    }
                }
            }
        }
    }
}
