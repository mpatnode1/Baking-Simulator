using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Customer : Person
    {
        public List<string> randomGreetings = new List<string>() { "Howdy.", "Hi.", "Hello.", "What's up?", "Hey.", "Sup." };


        public Customer(Game game) 
        {

            PersonCurrency = 1000;
            SetDefaultName(game);
            
        }

        public override void SetDefaultName(Game game)
        {

            this.PersonName = "Customer ";
            base.SetDefaultName(game);
        }

        public string CustomerIntroductionText()
        {
            string output = "";
            output = randomGreetings[new Random().Next(randomGreetings.Count)];
            output += $" I'm {PersonName}.";
            output += " What do you have for sale?";

            return output;
        }
    }
}
