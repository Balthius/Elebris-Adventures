using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Units
{
    public class ResourceUIValue : MonoBehaviour
    {
        public Image content;

        //Did not choose to add text on health/Resources from video #2.1

        public float MyMaxValue { get; set; }//Updating correctly to unique Values
        public float SetSize { get; set; }//Not Updating, stuck at 0

        private float lerpSpeed = 15f;
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
                float MaxVal = MyMaxValue * SetSize;
                currentFill = currentValue / MaxVal;
            }
        }
        // Use this for initialization
        void Start()
        {
            content = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentFill != content.fillAmount)
            {
                content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);

            }
        }
        public void Initialize(float currentValue, float maxValue, float setSize)

        {
            SetSize = setSize; //Sets the amount of the bar I want filled Always put your field alterations above your property and method calls if the proprty/method is used in that call!
            MyMaxValue = maxValue;
            MyCurrentValue = currentValue;
        }
    }
}