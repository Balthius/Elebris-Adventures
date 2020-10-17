using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Elebris.Library.Units;

namespace Assets.Scripts.Units
{
    public class UIResource : MonoBehaviour
    {
        private Slider content;//or image, down the road. slider just adds convenience for quick setup
        private TMP_Text valueText;

        ValueHolder _resourceValue;

        public float MyMaxValue { get; set; }//Updating correctly to unique Values

        private float lerpSpeed = 15f; //speed of health change
        private float currentValue;
        private float currentFill;

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
        void Awake()
        {
            content = GetComponent<Slider>();
            valueText = GetComponentInChildren<TMP_Text>();
            if(valueText = null)
            {
                Debug.Log("Null text in resource bar");
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            MyCurrentValue = _resourceValue.CurrentValue;
            if (currentFill != content.value)
            {
                //content.fillamount =  for when I switch to image.
                content.value = Mathf.Lerp(content.value, currentFill, Time.fixedDeltaTime * lerpSpeed);
                valueText.text = $"{MyCurrentValue}/{MyMaxValue}";
            }
        }
        public void Initialize(ValueHolder valueHolder)
        {
            Debug.Log(valueHolder + "Holds hp value");
            _resourceValue = valueHolder;
            MyMaxValue = _resourceValue.MaxValue;
            MyCurrentValue = _resourceValue.CurrentValue;

            valueText = GetComponentInChildren<TMP_Text>(); //eliminates a race condition
            valueText.text = $"{MyCurrentValue}/{MyMaxValue}";
        }
    }
}