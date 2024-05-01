using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProgrammingIICraftDemoPages
{
    public class Game
    {
        int shopInteractionCount = 0;
        CraftResults craftResult = CraftResults.craftNotActive;

        public Person Player = new Person()
        {
            Inventory = new List<Item>
        {
            new Item() {ItemName="Chocolate", ItemAmount = 3, ItemValue = 9.99, ItemAmountType = "pound(s)"},
            new Item() {ItemName="Water", ItemAmount = 10, ItemValue = .10, ItemAmountType = "cup(s)"},
            new Item(){ItemName = "Vanilla Extract", ItemAmount = 2, ItemAmountType = "tsp", ItemValue = 2.67 },
            new Item(){ItemName = "Milk", ItemAmount = 4, ItemAmountType="tbsps", ItemValue = 4.25 }
             }
        };
        public Person Vendor = new Person()
        {
            Inventory = new List<Item>
        {
            new Item() {ItemName="Chocolate", ItemAmount = 10, ItemAmountType = "pound(s)", ItemValue = 9.99},
            new Item(){ItemName = "Vanilla Extract", ItemAmount = 10, ItemAmountType = "tsp", ItemValue = 2.67 },
            new Item(){ItemName = "Milk", ItemAmount = 10, ItemAmountType="tbsps", ItemValue = 4.25 },
            new Item(){ItemName = "Powdered Sugar", ItemAmount = 10, ItemAmountType="cup", ItemValue = 1.95 },
            new Item(){ItemName = "Banana", ItemAmount = 10, ItemAmountType="cups", ItemValue=0.99},
            new Item(){ItemName = "Egg", ItemAmount = 10, ItemAmountType = "count", ItemValue = 3.89},
            new Item(){ItemName = "Cocoa Powder", ItemAmount = 10, ItemAmountType = "cup", ItemValue = 2.50},
            new Item(){ItemName = "Pudding Mix", ItemAmount = 10, ItemAmountType = "cups", ItemValue = 4.00},
            new Item(){ItemName = "Vanilla Wafer", ItemAmount = 10, ItemAmountType = "count", ItemValue = 1.50},
            new Item(){ItemName = "Flour", ItemAmount = 10, ItemAmountType="cup", ItemValue = 5.97 },
            new Item(){ItemName = "Sugar", ItemAmount = 10, ItemAmountType="cups", ItemValue = 3.49 },
             }
        };
        public string Space = "      ";

        public List<Recipe> Recipes = new List<Recipe>();

        public Game() 
        {
            RecipeSetUp();
            Vendor.SetDefaultName();
        }

        public bool CheckAbilityToCraft(Recipe activeRecipe)
        {
            //Recipe activeRecipe = Recipes.Find(x => x.RecipeName = selectedRecipe.ItemContent);
            if (activeRecipe != null)
            {
                foreach(Item itemInRecipe in activeRecipe.RecipeRequirements)
                {      
                        if (craftResult == CraftResults.craftFailure){ break; }
                        Item? itemInPlayerInventory = Player.Inventory.Find(x=> x.ItemName == itemInRecipe.ItemName);
                        
                        //checks if item is in inventory
                        if(itemInPlayerInventory != null)
                        {
                            //checks if theres enough of item
                            if(itemInPlayerInventory.ItemAmount >= itemInRecipe.ItemAmount)
                            {
                                craftResult = CraftResults.craftSuccess;
                            }
                            else
                            {
                                craftResult = CraftResults.craftFailure;
                            //TO DO: ADD FEEDBACK WHEN FAILED TO CRAFT
                        }
                        }
                        else
                        {
                            craftResult = CraftResults.craftFailure;
                        //TO DO: PLAYER DOES NOT HAVE ENOUGH OF ITEM
                        //ADD FEEDBACK
                        }
                   }
            }
            else 
            {
                craftResult = CraftResults.craftFailure;
            }
            //bool recipetry = Recipes.Any(p => p.RecipeName = selectedRecipe.ToString());

            //add the item to the player's inventory
            switch(craftResult) 
            {
                case CraftResults.craftFailure:
                    {
                        //TO DO: Add feedback
                        craftResult = CraftResults.craftNotActive;
                        return false;
                    }
                case CraftResults.craftSuccess:
                    {
                        foreach (Item itemInRecipe in activeRecipe.RecipeRequirements)
                        {
                            Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == itemInRecipe.ItemName);

                            itemInPlayerInventory.ItemAmount -= itemInRecipe.ItemAmount;
                            if (itemInPlayerInventory.ItemAmount <= 0)
                            {
                                Player.Inventory.Remove(itemInPlayerInventory);
                            }
                        }

                        Player.Inventory.Add(activeRecipe.CraftedRecipe);
                        craftResult = CraftResults.craftNotActive;
                        return true;
                        
                    }
            }

            return false;
        }

        public void CheckAbilityToBuyItems()
        {

            double itemsTotal = 0;

            //checks if player has enough currency for the items they want to buy
            foreach (Item item in Vendor.Inventory)
            {
                if (item.BuyingCount < 0)
                {
                    itemsTotal += item.ItemValue * item.BuyingCount;
                }
            }

            //adds item to player inventory and removes corresponding money amount
            if(itemsTotal <= Player.PersonCurrency)
            {
                foreach (Item item in Vendor.Inventory)
                {
                    //doesn't go through items that the player isn't buying
                    if(item.BuyingCount < 0)
                    {
                        Player.Inventory.Add(item);
                        

                        item.ItemAmount -= item.BuyingCount;

                        //removes item from vendor if they buy out inventory
                        if (item.ItemAmount <= 0)
                        {
                            Vendor.Inventory.Remove(item);

                        }

                        
                    }
                }
            }
            else
            {
                //TO DO ADD RETURN FALSE
            }
        }

        public string VendorIntroduction()
        {
            string output = "";
            if (shopInteractionCount == 0)
            {
                output += $"Welcome to my shop {Player.PersonName}. My name is {Vendor.PersonName}.\n";
                shopInteractionCount++;
                //TO DO when buy button is complete move shop interaction ++ to there
            }
            else
            {
                output += $"Would you like to buy anything else, {Player.PersonName}?\n";
                output += $"These are the wares I have left:\n";
            }
            return output;
        }

        public void RecipeSetUp()
        {
            //runs at start of game and adds recipes to list of recipes

            //recipe for Powdered Sugar Icing
            Recipes.Add(
                  new Recipe()
                  {
                      CraftedRecipe = new Item()
                      {
                          ItemName = "Powdered Sugar Icing",
                          ItemValue = 5.97,
                          ItemAmount = 8,
                          ItemAmountType = "cups",
                      }, 

                      RecipeName = "Powdered Sugar Icing",
                      RecipeDescription = "Tasty icing that can be used with many recipes.",
                      RecipeValue = 5.97,
                      RecipeAmount = 8,
                      RecipeAmountType = "cups",

                  RecipeRequirements = new List<Item>()
                      {
                        new Item(){ItemName = "Powdered Sugar", ItemAmount = 1, ItemAmountType="cup", ItemValue = 1.95 },
                        new Item(){ItemName = "Vanilla Extract", ItemAmount = 1.5, ItemAmountType = "tsp", ItemValue = 2.67 },
                        new Item(){ItemName = "Milk", ItemAmount = 2, ItemAmountType="tbsps", ItemValue = 4.25 }
                      },
                  }
                  );

            Recipes.Add(
                new Recipe()
                {
                    CraftedRecipe = new Item()
                    {
                        ItemName = "Chocolate Cake",
                        ItemValue = 10.60,
                        ItemAmount = 4,
                        ItemAmountType = "slices",
                    },

                    RecipeName = "Chocolate Cake",
                    RecipeDescription = "A classic cake with chocolate flavoring.",
                    RecipeValue = 10.60,
                    RecipeAmount = 4,
                    RecipeAmountType = "slices",

                RecipeRequirements = new List<Item>()
                    {
                        new Item(){ItemName = "Powdered Sugar Icing", ItemAmount = 1.5, ItemAmountType="cups", ItemValue = 5.97 },
                        new Item(){ItemName = "Banana", ItemAmount = 2, ItemAmountType="cups", ItemValue=0.99},
                        new Item(){ItemName = "Egg", ItemAmount = 3, ItemAmountType = "count", ItemValue = 3.89},
                        new Item(){ItemName = "Cocoa Powder", ItemAmount = .5, ItemAmountType = "cup", ItemValue = 2.50}
                    }
                }
                );

            Recipes.Add(
                new Recipe()
                {
                    CraftedRecipe = new Item()
                    {
                        ItemName = "Banana Pudding",
                        ItemValue = 4.50,
                        ItemAmount = 10,
                        ItemAmountType = "cups",
                    },

                    RecipeName = "Banana Pudding",
                    RecipeDescription = "Did you hear about the dessert that was made with a bad banana? It was very off-pudding.",
                    RecipeValue = 4.50,
                    RecipeAmount = 10,
                    RecipeAmountType = "cups",

                RecipeRequirements = new List<Item>()
                    {
                        new Item(){ItemName = "Milk", ItemAmount = 2, ItemAmountType="tbsps", ItemValue = 4.25 },
                        new Item(){ItemName = "Banana", ItemAmount = 2, ItemAmountType="cups", ItemValue=0.99},
                        new Item(){ItemName = "Pudding Mix", ItemAmount = 4, ItemAmountType = "cups", ItemValue = 4.00},
                        new Item(){ItemName = "Vanilla Wafer", ItemAmount = 6, ItemAmountType = "count", ItemValue = 1.50}
                    }
                }
                );

            Recipes.Add(
             new Recipe()
             {
                 CraftedRecipe = new Item()
                 {
                     ItemName = "Frosted Sugar Cookie",
                     ItemValue = 2.50,
                     ItemAmount = 12,
                     ItemAmountType = "count",
                 },

                 RecipeName = "Frosted Sugar Cookie",
                 RecipeDescription = "You can cut them into so many shapes! (With your imagination).",
                 RecipeValue = 2.50,
                 RecipeAmount =  12,
                 RecipeAmountType = "count",
                 RecipeRequirements = new List<Item>()
                 {
                      new Item(){ItemName = "Powdered Sugar Icing", ItemAmount = 1.5, ItemAmountType="cups", ItemValue = 5.97 },
                      new Item(){ItemName = "Egg", ItemAmount = 1.5, ItemAmountType="cup", ItemValue = 5.97 },
                      new Item(){ItemName = "Flour", ItemAmount = 1.5, ItemAmountType="cup", ItemValue = 5.97 },
                      new Item(){ItemName = "Sugar", ItemAmount = 3, ItemAmountType="cups", ItemValue = 3.49 },
                 }
             }
             );

        }


        public string GetRecipeList()
        {
            int number = 1;
            string output = "Recipes:\n\n";
            foreach (Recipe recipe in Recipes)
            {
                output += $"  {number}. {recipe.GetRecipeDescription()}";
                number++;
                
            }
           
            return output;
        }
    }
}
