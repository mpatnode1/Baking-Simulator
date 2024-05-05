using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingIICraftDemoPages
{
    public class Item : INotifyPropertyChanged
    {

        public string ItemName { get; set; }
        public string ItemDescription 
        {
            get { return GetItemDescription(); }
            set { } 
        }

        public double ItemValue = 0;
        public double ItemAmount = 1;
        public string ItemAmountType = "cup(s)";
        public double ItemQualityMultipler = 1;
        public string ItemQuality = "Common";

        private List<double> craftingProbabilityList = new List<double> { 1, 1, 1, 1, 1, 1, 1, 1.7, 1.4, 1.4 };

        public event PropertyChangedEventHandler? PropertyChanged;

        public double CurrentItemValue { get; set; }
        private int buyingCount;
        public int BuyingCount { get { return buyingCount; } set { buyingCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingCount))); } }
        public ChangeBuyCount IncreaseCountCommand { get; set; }
        public ChangeBuyCount DecreaseCountCommand { get; set; }


        public Item()
        {
            IncreaseCountCommand = new ChangeBuyCount(this, 1); 
            DecreaseCountCommand = new ChangeBuyCount(this, -1);

            IncreaseCountCommand.siblingChanged = DecreaseCountCommand.Changed;
            DecreaseCountCommand.siblingChanged = IncreaseCountCommand.Changed;
        }
        public void QualityMultiplerCalculator()
        {
            Random rnd = new Random();
            int valueMultiplierIndex = rnd.Next(craftingProbabilityList.Count);
            double valueMultiplier = craftingProbabilityList[valueMultiplierIndex];

            if(valueMultiplier == 1)
            {
                return;
            }
            else if (valueMultiplier == 1.4)
            {
                ItemValue *= valueMultiplier;
                ItemName = $"Uncommon " + ItemName;
            }
            else if(valueMultiplier == 1.7)
            {
                ItemValue *= valueMultiplier;
                ItemName = $"Rare " + ItemName;
            }
        }
        public Item GetMemberwiseClone()
        {
            return (Item)this.MemberwiseClone();
        }
        public string GetItemDescription()
        {
            return $"{ItemAmount} {ItemAmountType} {ItemName} ({ItemValue.ToString("C")} ea)";
        }
    
        public double ValueForCurrentItemAmount()
        {
            CurrentItemValue = ItemAmount * ItemValue;
            return CurrentItemValue;
        }

        public void CheckMeasurementPlurality()
        {
            if (ItemAmount == 1 && ItemAmountType == "cups")
            {
                ItemAmountType = "cup";
            }
            else if (ItemAmount > 1 && ItemAmountType == "cup")
            {
                ItemAmountType = "cups";
            }

            if (ItemAmount == 1 && ItemAmountType == "tsps")
            {
                ItemAmountType = "tsp";
            }
            else if (ItemAmount > 1 && ItemAmountType == "tsp")
            {
                ItemAmountType = "tsps";
            }

            if (ItemAmount == 1 && ItemAmountType == "tbsps")
            {
                ItemAmountType = "tbsp";
            }
            else if (ItemAmount > 1 && ItemAmountType == "tbsp")
            {
                ItemAmountType = "tbsps";
            }

        }
    }

    public class ChangeBuyCount : ICommand
    {
        public event EventHandler? CanExecuteChanged;


        private Item parentItem;
        private int changeAmountValue;

        public ChangeBuyCount(Item parentItem, int changeAmountValue)
        {
            this.parentItem = parentItem;
            this.changeAmountValue = changeAmountValue;
        }

        public delegate void SiblingChanged();

        public SiblingChanged? siblingChanged;
        public void Changed()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            int newCount = changeAmountValue + parentItem.BuyingCount;

            Debug.WriteLine($"{changeAmountValue}: {newCount} ~ {!(newCount > parentItem.ItemAmount || newCount < 0)}");

            return !(newCount > parentItem.ItemAmount || newCount < 0);
        }

        public void Execute(object? parameter)
        {
            parentItem.BuyingCount += changeAmountValue;
            Debug.WriteLine(parentItem.BuyingCount);

            //CommandManager.InvalidateRequerySuggested();
            
            CanExecuteChanged?.Invoke(this, new EventArgs());
            if(siblingChanged != null) { siblingChanged(); }
            

            // new PropertyChangedEventArgs(nameof(parentItem.BuyingCount))
        }
    }
}
