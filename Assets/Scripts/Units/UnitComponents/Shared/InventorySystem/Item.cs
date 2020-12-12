using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Units.UnitComponents.Shared
{

    // ScriptableItem + Amount is useful for default items (e.g. spawn with 10 potions)
    [Serializable]
    public struct ScriptableItemAndAmount
    {
        public ScriptableItem item;
        public int amount;
    }
    [Serializable]
    public partial struct Item
    {

        ScriptableItem _data;
        public Item(ScriptableItem data)
        {
            _data = data;

        }

        // wrappers for easier access
        public ScriptableItem data
        {
            get
            {
                return data;
            }
        }
        public string name => data.name;
        public int maxStack => data.maxStack;
        public long buyPrice => data.buyPrice;
        public long sellPrice => data.sellPrice;
        public long itemMallPrice => data.itemMallPrice;
        public bool sellable => data.sellable;
        public bool tradable => data.tradable;
        public bool destroyable => data.destroyable;
        public Sprite image => data.image;

        // tooltip
        public string ToolTip()
        {
            // we use a StringBuilder so that addons can modify tooltips later too
            // ('string' itself can't be passed as a mutable object)
            StringBuilder tip = new StringBuilder(data.ToolTip());
            return tip.ToString();
        }
    }

}
