using Assets.DapperEvents.GameEvents;
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(menuName ="UI/ConstantManager")]
    public class ConstantPopupManager : ScriptableObject
    {
        public TextModel damageModel;
        public TextModel healModel;
        public Action<Transform, TextBundle> OnValueChanged;

        public void DisplayDamage(Transform transform, string value)
        {
           
            OnValueChanged(transform, new TextBundle(damageModel, value));
        }
        public void DisplayHealing(Transform transform, string value)
        {

            OnValueChanged(transform, new TextBundle(healModel, value));
        }

        public void InterpretValue(Transform unit, float value)
        {
            if(value > 0)
            {
                DisplayHealing(unit, value.ToString());
            }
            else
            {
                DisplayDamage(unit, value.ToString());
            }
        }    


    }
}
