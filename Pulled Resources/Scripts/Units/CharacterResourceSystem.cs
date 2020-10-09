
using System.Collections.Generic;
using System;
using Elebris.Library.Values;
using Elebris.Library.Units.Interfaces;
using UnityEngine;

namespace Elebris.Library.Units
{
    public class CharacterResourceSystem : IHealable
    {

        //Store all 
        private List<AttributeObject> storedAttributes;
        public List<AttributeObject> StoredAttributes
        {
            get => storedAttributes;
            set
            {
                storedAttributes = value;
            }
        }
        private List<StatObject> storedStats;
        public List<StatObject> StoredStats
        {
            get => storedStats;
            set
            {
                storedStats = value;
            }
        }

        public void InititalizeResourceLists()
        {
            storedAttributes = new List<AttributeObject>();
            storedStats = new List<StatObject>();
        }

        //add SO for experienceprogression
        //public StatData HealthData = null;
        //public StatData StaminaData = null;
        //public StatData ManaData = null;
        //public StatData SpiritData = null;

        private ValueHolder characterHealth;
        private ValueHolder characterSpirit;
        private ValueHolder characterStamina;
        private ValueHolder characterMana;
        public ValueHolder CharacterHealth {
            get => characterHealth;
            set
            {
                characterHealth = value;
                if (CharacterHealth.currentValue <= 0)
                {
                    //Die
                }
            }
        }
        public ValueHolder CharacterSpirit { get => characterSpirit; set => characterSpirit = value; }
        public ValueHolder CharacterStamina { get => characterStamina; set => characterStamina = value; }
        public ValueHolder CharacterMana { get => characterMana; set => characterMana = value; }
       
        public void Heal(int amount)
        {
            CharacterHealth.currentValue += Mathf.Min(CharacterHealth.maxValue, CharacterHealth.currentValue + amount);
        }

        public void GenerateShield(int amount)
        {
            //CurrentShield += amount;
        }
        public void UpdateUIValues()
        {//can use the event system in character
            //maxHealth = character.characterStats.RetrieveValue(StatsEnum.MaximumHealth).TotalValue;
        }
        public void ReceiveDamage(GameObject attacker)
        {
            //defenderCombatStats.currentDurability -= combatHandler.CalculateDurabilityLoss(defenderCombatStats.gearTier, attackerCombatStats.gearTier);
        }
        private void RecalculateResources()
        {
            //I need to make sure these holders dont overwrite any modifiers on the stat
            //update on levelup, gear change etc, calculate last
            //CharacterHealth = new ValueHolder(RetrieveStatObjectFromList(StoredStats, HealthData).TotalValue, characterHealth.missingValue, HealthData);
            //CharacterSpirit = new ValueHolder(RetrieveStatObjectFromList(StoredStats, SpiritData).TotalValue, characterSpirit.missingValue, SpiritData);
            //CharacterMana = new ValueHolder(RetrieveStatObjectFromList(StoredStats, ManaData).TotalValue, characterMana.missingValue, ManaData);
            //CharacterStamina = new ValueHolder(RetrieveStatObjectFromList(StoredStats, StaminaData).TotalValue, characterStamina.missingValue, StaminaData);
        }
        public StatObject RetrieveStatObjectFromList(List<StatObject> list, StatsEnum value)
        {//retrieve a matching value from the characters stored stats
            foreach (StatObject storedValue in list)
            {
                if (storedValue.statType == value)
                {
                    return storedValue;
                }
            }
            return null;
        }

        public void ReceiveDamage(IDamager attacker)
        {
            throw new NotImplementedException();
        }
    }
    
}