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


        public Customer() 
        {

            PersonCurrency = 1000;
            SetDefaultName();
            
        }

        public override void SetDefaultName()
        {

            this.PersonName = "Customer ";
            base.SetDefaultName();
        }

        public string CustomerIntroductionText()
        {
            string output = "";
            output = randomGreetings[new Random().Next(randomGreetings.Count)];
            output += $" My name is {PersonName}.";
           // pull rest of introduction from text file

            return output;
        }
    }
}
