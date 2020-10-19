using UnityEngine;
using TMPro;
using Elebris.Library.Units;

namespace Assets.Scripts.Units
{
    public class UIUnitText : MonoBehaviour
    {
        ValueHolder _resourceValue;

        public float MyMaxValue { get; set; }//Updating correctly to unique Values
        public float MyCurrentValue { get; private set; }

        private float lerpSpeed = 15f; //speed of health change
        private float currentValue;
        private TMP_Text valueText;
        void Awake()
        {
            valueText = GetComponentInChildren<TMP_Text>();
        }

        void FixedUpdate()
        {
            MyCurrentValue = _resourceValue.CurrentValue;
           
            //valueText.text = $"{MyCurrentValue}/{MyMaxValue}"; // Also displays max
            valueText.text = $"{MyCurrentValue}"; //Only displays current
            //change text color at max
            
        }
        public void Initialize(ValueHolder valueHolder)
        {
            _resourceValue = valueHolder;
            MyMaxValue = _resourceValue.MaxValue;
            MyCurrentValue = _resourceValue.CurrentValue;
        }
    }
}