using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ProgrammingIICraftDemoPages
{
    public class Game
    {
        
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
        public Vendor Vendor = new Vendor()
        {
            Inventory = new List<Item>
        {
            new Item() {ItemName="Chocolate", ItemAmount = 5, ItemAmountType = "pound(s)", ItemValue = 9.99},
            new Item(){ItemName = "Vanilla Extract", ItemAmount = 5, ItemAmountType = "tsp", ItemValue = 2.67 },
            new Item(){ItemName = "Milk", ItemAmount = 5, ItemAmountType="tbsps", ItemValue = 4.25 },
            new Item(){ItemName = "Powdered Sugar", ItemAmount = 5, ItemAmountType="cup", ItemValue = 1.95 },
            new Item(){ItemName = "Banana", ItemAmount = 5, ItemAmountType="cups", ItemValue=0.99},
            new Item(){ItemName = "Egg", ItemAmount = 5, ItemAmountType = "count", ItemValue = 2.89},
            new Item(){ItemName = "Cocoa Powder", ItemAmount = 5, ItemAmountType = "cup", ItemValue = 2.50},
            new Item(){ItemName = "Pudding Mix", ItemAmount = 5, ItemAmountType = "cups", ItemValue = 4.00},
            new Item(){ItemName = "Vanilla Wafer", ItemAmount = 5, ItemAmountType = "count", ItemValue = 1.50},
            new Item(){ItemName = "Flour", ItemAmount = 5, ItemAmountType="cup", ItemValue = 5.97 },
            new Item(){ItemName = "Sugar", ItemAmount = 5, ItemAmountType="cups", ItemValue = 3.49 },
            }
        };

        public string Space = "      ";

        public List<Recipe> Recipes = new List<Recipe>();

        string filePathForNames = "../../../data/DefaultNamesList.txt";
        public List<string> defaultNames = new List<string>();

        //this is referenced to "restock" vendor's inventory
        public List<Item> AllActiveItemsInGame = new List<Item>() 
        {
            new Item() {ItemName="Chocolate", ItemAmount = 1, ItemAmountType = "pound(s)", ItemValue = 9.99},
            new Item(){ItemName = "Vanilla Extract", ItemAmount = 1, ItemAmountType = "tsp", ItemValue = 2.67 },
            new Item(){ItemName = "Milk", ItemAmount = 1, ItemAmountType="tbsps", ItemValue = 4.25 },
            new Item(){ItemName = "Powdered Sugar", ItemAmount = 1, ItemAmountType="cup", ItemValue = 1.95 },
            new Item(){ItemName = "Banana", ItemAmount = 1, ItemAmountType="cups", ItemValue=0.99},
            new Item(){ItemName = "Egg", ItemAmount = 1, ItemAmountType = "count", ItemValue = 2.89},
            new Item(){ItemName = "Cocoa Powder", ItemAmount = 1, ItemAmountType = "cup", ItemValue = 2.50},
            new Item(){ItemName = "Pudding Mix", ItemAmount = 1, ItemAmountType = "cups", ItemValue = 4.00},
            new Item(){ItemName = "Vanilla Wafer", ItemAmount = 1, ItemAmountType = "count", ItemValue = 1.50},
            new Item(){ItemName = "Flour", ItemAmount = 1, ItemAmountType="cup", ItemValue = 5.97 },
            new Item(){ItemName = "Sugar", ItemAmount = 1, ItemAmountType="cups", ItemValue = 3.49 },
        };

        public int VendorRestockCounter = 0;
        public Game() 
        {
            LoadNameData();
            RecipeSetUp();
            Vendor.SetDefaultName(this);
            Vendor.RestockVendor(this);
        }

        public void LoadNameData()
        {
            defaultNames = File.ReadAllLines(filePathForNames).ToList();
        }
        public bool CheckAbilityToCraft(Recipe activeRecipe)
        {
            if (activeRecipe != null)
            {
                //goes through each item in recipe requirements
                foreach (Item itemInRecipe in activeRecipe.RecipeRequirements)
                {

                    Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == itemInRecipe.ItemName);

                    //checks if item is in inventory
                    if (itemInPlayerInventory != null)
                    {
                        //checks if theres enough of item
                        if (itemInPlayerInventory.ItemAmount >= itemInRecipe.ItemAmount)
                        {
                            craftResult = CraftResults.craftSuccess;
                        }
                        else
                        {
                            craftResult = CraftResults.craftFailure;
                        }
                    }
                    else
                    {
                        craftResult = CraftResults.craftFailure;
                    }

                    //breaks for loop if any of the items in foreach loop fail
                    if (craftResult == CraftResults.craftFailure) { break; }
                }
            }
            else
            {
                craftResult = CraftResults.craftFailure;
            }

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

                        if (Player.Inventory.Any(x => x.ItemName == activeRecipe.CraftedRecipe.ItemName))
                        {
                            Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == activeRecipe.CraftedRecipe.ItemName);
                            if (itemInPlayerInventory != null)
                            {
                                itemInPlayerInventory.ItemAmount += activeRecipe.CraftedRecipe.ItemAmount;
                            }
                        }
                        else
                        {
                            Item cloneForPlayerInventory = activeRecipe.CraftedRecipe.GetMemberwiseClone();
                            Player.Inventory.Add(cloneForPlayerInventory);

                            Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == activeRecipe.CraftedRecipe.ItemName);
                            if (itemInPlayerInventory != null)
                            {
                                itemInPlayerInventory.ItemAmount = activeRecipe.CraftedRecipe.ItemAmount;
                            }
                     
                        }
                        craftResult = CraftResults.craftNotActive;
                        return true;
                        
                    }
            }

            return false;
        }

        public int CheckAbilityToBuyItems()
        {

            double itemsTotal = 0;
            bool itemFound = false;
            List<Item> ToDelete = new List<Item>();

            //checks if player has enough currency for the items they want to buy
            foreach (Item item in Vendor.Inventory)
            {
                if (item.BuyingCount > 0)
                {
                    itemsTotal += item.ItemValue * item.BuyingCount;
                    itemFound = true;
                }
            }

            if (itemFound == false)
            {
                Debug.WriteLine("No items found with buying count higher than 1");
                return 3;
            }
                        
            //adds item to player inventory and removes corresponding money amount
            if (itemsTotal <= Player.PersonCurrency)
            {
                Debug.WriteLine("Player has enough currency.");
                foreach (Item item in Vendor.Inventory)
                {

                    //doesn't go through items that the player isn't buying
                    if(item.BuyingCount > 0)
                    {
                        Debug.WriteLine(item.BuyingCount);
                        //if player has item in inventory add to item amount
                        //if not create new item

                        if (Player.Inventory.Any(x => x.ItemName == item.ItemName))
                        {
                            Debug.WriteLine("Item found in player inventory.");
                            Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == item.ItemName);
                            if (itemInPlayerInventory != null) 
                            {
                                itemInPlayerInventory.ItemAmount += item.BuyingCount;
                                itemInPlayerInventory.BuyingCount = 0;
                                Debug.WriteLine("Item amount for preexisting item in player inventory was increased.");
                            }
                            
                            
                        }
                        else
                        {
                            Item cloneForPlayerInventory = item.GetMemberwiseClone();
                            Player.Inventory.Add(cloneForPlayerInventory);

                            Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == item.ItemName);
                            if (itemInPlayerInventory != null)
                            {
                                itemInPlayerInventory.ItemAmount = item.BuyingCount;
                                itemInPlayerInventory.BuyingCount = 0;
                                Debug.WriteLine("Item added to player inventory was found.");
                            }
                            Debug.WriteLine("New item was added to player inventory.");
                        }
                        item.ItemAmount -= item.BuyingCount;
                        item.BuyingCount = 0;

                        //removes item from vendor if they buy out inventory
                        if (item.ItemAmount <= 0)
                        {
                            ToDelete.Add(item);
                            Debug.WriteLine("Removing item from vendor inventory.");
                        }

                       
                    }

                }

                foreach(Item item in ToDelete)
                {
                    Vendor.Inventory.Remove(item);    
                }

                Vendor.ShopInteractionCount++;
                Player.PersonCurrency -= itemsTotal;
                return 1;
            }
            else
            {
                return 2;
            }
          
        }

        public bool SellItemToCustomer(Item selectedItem)
        {
            if (Player.Inventory.Any(x => x.ItemName == selectedItem.ItemName))
            {
                Item? itemInPlayerInventory = Player.Inventory.Find(x => x.ItemName == selectedItem.ItemName);
                if (itemInPlayerInventory != null)
                {
                    
                    if (itemInPlayerInventory.ItemAmount >= 1)
                    {
                        Player.PersonCurrency += itemInPlayerInventory.ItemValue;
                        itemInPlayerInventory.ItemAmount--;
                    }
                    else if(itemInPlayerInventory.ItemAmount < 1 && itemInPlayerInventory.ItemAmount > 0)
                    {
                        itemInPlayerInventory.ValueForCurrentItemAmount();
                        Player.PersonCurrency += itemInPlayerInventory.CurrentItemValue;
                        itemInPlayerInventory.ItemAmount = 0;

                    }
                    
                    if (itemInPlayerInventory.ItemAmount <= 0)
                    {
                        Player.Inventory.Remove(itemInPlayerInventory);
                    }
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
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
                        new Item(){ItemName = "Egg", ItemAmount = 3, ItemAmountType = "count", ItemValue = 2.89},
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
                      new Item(){ItemName = "Egg", ItemAmount = 1.5, ItemAmountType="count", ItemValue = 2.89 },
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
    
        public Customer GetCustomer()
        {
            Customer currentCustomer = new Customer(this);
            currentCustomer.SetDefaultName(this);
            return currentCustomer; 
        }
    }
}
