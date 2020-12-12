using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Units.UnitComponents.Shared
{
    [Serializable]
    public partial struct ItemSlot
    {
        public Item item;
        public int amount;

        // constructors
        public ItemSlot(Item item, int amount = 1)
        {
            this.item = item;
            this.amount = amount;
        }

        // helper functions to increase/decrease amount more easily
        // -> returns the amount that we were able to increase/decrease by
        public int DecreaseAmount(int reduceBy)
        {
            // as many as possible
            int limit = Mathf.Clamp(reduceBy, 0, amount);
            amount -= limit;
            return limit;
        }

        public int IncreaseAmount(int increaseBy)
        {
            // as many as possible
            int limit = Mathf.Clamp(increaseBy, 0, item.maxStack - amount);
            amount += limit;
            return limit;
        }

        // tooltip
        public string ToolTip()
        {
            if (amount == 0) return "";

            // we use a StringBuilder so that addons can modify tooltips later too
            // ('string' itself can't be passed as a mutable object)
            StringBuilder tip = new StringBuilder(item.ToolTip());
            tip.Replace("{AMOUNT}", amount.ToString());
            return tip.ToString();
        }
    }

}
