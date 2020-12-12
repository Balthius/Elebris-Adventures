using Elebris.Core.Library.Enums.Tags;
using Elebris.Core.Library.Items.Equipment;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units.UnitComponents.Shared
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("Inventory")]
        public List<IEquippable> equippedGear = new List<IEquippable>();
        public List<ItemSlot> inventory = new List<ItemSlot>();
        public Dictionary<EquipSlot, List<ItemSlot>> storedEquipment = new Dictionary<EquipSlot, List<ItemSlot>>();
        public List<ItemSlot> storedMaterials = new List<ItemSlot>(); //may later switch to dict and separate by type (metal, stone, wood, plant, monster etc)

        // all entities should have gold, not just the player
        // useful for monster loot, chests etc.
        // note: int is not enough (can have > 2 mil. easily)
        [Header("Gold")]
        [SerializeField] long _gold = 0;
        public long gold { get { return _gold; } set { _gold = Math.Max(value, 0); } }
        public int GetInventoryIndexByName(string itemName)
        {
            
            // (avoid FindIndex to minimize allocations)
            for (int i = 0; i < inventory.Count; ++i)
            {
                ItemSlot slot = inventory[i];
                if (slot.amount > 0 && slot.item.name == itemName)
                    return i;
            }
            return -1;
        }

        // helper function to count the free slots
        public int InventorySlotsFree()
        {
            // count manually. Linq is HEAVY(!) on GC and performance
            int free = 0;
            foreach (ItemSlot slot in inventory)
                if (slot.amount == 0)
                    ++free;
            return free;
        }

        // helper function to calculate the occupied slots
        public int InventorySlotsOccupied()
        {
            // count manually. Linq is HEAVY(!) on GC and performance
            int occupied = 0;
            foreach (ItemSlot slot in inventory)
                if (slot.amount > 0)
                    ++occupied;
            return occupied;
        }
        public int InventoryCount(Item item)
        {
            // count manually. Linq is HEAVY(!) on GC and performance
            int amount = 0;
            foreach (ItemSlot slot in inventory)
                if (slot.amount > 0 && slot.item.Equals(item))
                    amount += slot.amount;
            return amount;
        }

        // helper function to remove 'n' items from the inventory
        public bool InventoryRemove(Item item, int amount)
        {
            for (int i = 0; i < inventory.Count; ++i)
            {
                ItemSlot slot = inventory[i];
                // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                if (slot.amount > 0 && slot.item.Equals(item))
                {
                    // take as many as possible
                    amount -= slot.DecreaseAmount(amount);
                    inventory[i] = slot;

                    // are we done?
                    if (amount == 0) return true;
                }
            }

            // if we got here, then we didn't remove enough items
            return false;
        }
        // helper function to check if the inventory has space for 'n' items of type
        // -> the easiest solution would be to check for enough free item slots
        // -> it's better to try to add it onto existing stacks of the same type
        //    first though
        // -> it could easily take more than one slot too
        // note: this checks for one item type once. we can't use this function to
        //       check if we can add 10 potions and then 10 potions again (e.g. when
        //       doing player to player trading), because it will be the same result
        public bool InventoryCanAdd(Item item, int amount)
        {
            // go through each slot
            for (int i = 0; i < inventory.Count; ++i)
            {
                // empty? then subtract maxstack
                if (inventory[i].amount == 0)
                    amount -= item.maxStack;
                // not empty. same type too? then subtract free amount (max-amount)
                // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                else if (inventory[i].item.Equals(item))
                    amount -= (inventory[i].item.maxStack - inventory[i].amount);

                // were we able to fit the whole amount already?
                if (amount <= 0) return true;
            }

            // if we got here than amount was never <= 0
            return false;
        }

        // helper function to put 'n' items of a type into the inventory, while
        // trying to put them onto existing item stacks first
        // -> this is better than always adding items to the first free slot
        // -> function will only add them if there is enough space for all of them
        public bool InventoryAdd(Item item, int amount)
        {
            // we only want to add them if there is enough space for all of them, so
            // let's double check
            if (InventoryCanAdd(item, amount))
            {
                // add to same item stacks first (if any)
                // (otherwise we add to first empty even if there is an existing
                //  stack afterwards)
                for (int i = 0; i < inventory.Count; ++i)
                {
                    // not empty and same type? then add free amount (max-amount)
                    // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                    if (inventory[i].amount > 0 && inventory[i].item.Equals(item))
                    {
                        ItemSlot temp = inventory[i];
                        amount -= temp.IncreaseAmount(amount);
                        inventory[i] = temp;
                    }

                    // were we able to fit the whole amount already? then stop loop
                    if (amount <= 0) return true;
                }

                // add to empty slots (if any)
                for (int i = 0; i < inventory.Count; ++i)
                {
                    // empty? then fill slot with as many as possible
                    if (inventory[i].amount == 0)
                    {
                        int add = Mathf.Min(amount, item.maxStack);
                        inventory[i] = new ItemSlot(item, add);
                        amount -= add;
                    }

                    // were we able to fit the whole amount already? then stop loop
                    if (amount <= 0) return true;
                }
                // we should have been able to add all of them
                if (amount != 0) Debug.LogError("inventory add failed: " + item.name + " " + amount);
            }
            return false;
        }
        // equipment ///////////////////////////////////////////////////////////////
        //public int GetEquipmentIndexByName(string itemName)
        //{
        //    return equippedGear.FindIndex(slot => slot.amount > 0 && slot.item.name == itemName);
        //}

        //// helper function to find the equipped weapon index
        //// -> works for all entity types. returns -1 if no weapon equipped.
        //public int GetEquippedWeaponIndex()
        //{
        //    return equippedGear.FindIndex(slot => slot.amount > 0 &&
        //                                       slot.item.data is WeaponItem);
        //}

        //// get currently equipped weapon category to check if skills can be casted
        //// with this weapon. returns "" if none.
        //public string GetEquippedWeaponCategory()
        //{
        //    return "";
        //}

        // death ///////////////////////////////////////////////////////////////////
    }

}
