using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(menuName = "Actions/New Basic Passive")]
    [Serializable]
    public class PassiveScriptableObject : ScriptableObject
    {
        public PassiveBehaviorModel model;

        UnitPassiveComponent[] components;


        public virtual void GenerateComponents(Unit owner)
        {

            foreach (var item in components)
            {
                owner.PassiveContainer.passives.Add(item);
            }
        }

    }


}