using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Vendor : Person
    {
        int shopInteractionCount = 0;
        public Vendor() 
        { 
                   
        }

        public override void SetDefaultName()
        {

            this.PersonName = "ShopKeep ";
            base.SetDefaultName();
        }

        public string VendorIntroduction(Game game)
        {

            string output = "";
            if (shopInteractionCount == 0)
            {
                output += $"Welcome to my shop {game.Player.PersonName}. My name is {PersonName}.\n";
                shopInteractionCount++;
                //TO DO when buy button is complete move shop interaction ++ to there
            }
            else
            {
                output += $"Would you like to buy anything else, {game.Player.PersonName}?\n";
                output += $"These are the wares I have left:\n";
            }
            return output;
        }
    }
}
