using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Recipe
    {
        public string RecipeName = "";
        public string RecipeDescription = "";
        public double RecipeValue = 0;
        public double RecipeAmount = 1;
        public string RecipeAmountType = "cup(s)";
        public List<Item> RecipeRequirements = new List<Item>();

        public string GetRecipeDescription()
        {
            string space = "      ";
            string output = "";
            output += $"{RecipeName}\n{space}{RecipeDescription}\n{space}Makes {RecipeAmount} {RecipeAmountType} ({RecipeValue.ToString("C")} ea)\n\n{space}Requirements:\n";
            foreach (Item item in RecipeRequirements)
            {
                output += $"{space}{space}* {item.GetItemDescription()}";
            }
            return output;
        }
    }
}
