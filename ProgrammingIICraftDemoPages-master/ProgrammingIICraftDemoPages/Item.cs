﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingIICraftDemoPages
{
    public class Item
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
        public double CurrentItemValue { get; set; }
        public int BuyingCount {get; set; }

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
}
