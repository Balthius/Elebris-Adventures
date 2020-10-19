using Elebris.Library.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Units
{
    public class UIUnitBar : MonoBehaviour
    {
        ValueHolder _resourceValue;
        //this is not currently working as intended, but doesnt impact gameplay either. 
        //at some point I'll either add a color changing system for each level of charge, or work out the fillamount properly.
        public float MyMaxValue { get; set; }//Updating correctly to unique Values

        private float lerpSpeed = 15f; //speed of health change
        private float currentValue;
        private float currentFill;
        private Image _unitBar;
        void Awake()
        {
            _unitBar = GetComponentInChildren<Image>();
        }

        public float MyCurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                //these checks should be uncessesary since the player values also check for this, but better safe than sorry
                if (value < 0)
                {
                    currentValue = 0;
                }
                else if (value > MyMaxValue)
                {
                    currentValue = MyMaxValue;
                }
                else
                {
                    currentValue = value;
                }
                currentFill = currentValue / MyMaxValue; //value between 0 and 1
            }
        }
        void FixedUpdate()
        {
            MyCurrentValue = _resourceValue.CurrentValue;
            MyMaxValue = _resourceValue.MaxValue;
            Debug.Log($"Current value {MyCurrentValue} and max {MyMaxValue}");
            if (currentFill != _unitBar.fillAmount)
            {
                _unitBar.fillAmount = Mathf.Lerp(_unitBar.fillAmount, currentFill, Time.fixedDeltaTime * lerpSpeed);
            }
        }
        public void Initialize(ValueHolder valueHolder)
        {

            _resourceValue = valueHolder;
            MyMaxValue = _resourceValue.MaxValue;
            MyCurrentValue = _resourceValue.CurrentValue;
        }
    }
}