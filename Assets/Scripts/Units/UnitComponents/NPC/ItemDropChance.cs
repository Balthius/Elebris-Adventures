using Assets.Scripts.Units.UnitComponents.Shared;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [Serializable]
    public class ItemDropChance
    {
        public ScriptableItem item;
        [Range(0, 1)] public float probability;
    }

}