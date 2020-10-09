using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentPanel : MonoBehaviour
{

    Unit _activeUnit;
    public void Initialize(Unit activeUnit)
    {
        _activeUnit = activeUnit;
    }

    public void DecreaseHealth()
    {

        //these can be moved into a "changehealth" method with a parameter of int/float in order to crunch this down to 1 method
        _activeUnit.ResourceContainer.HealthValue.currentValue -= 5;
        DisplayStats();
    }
    public void IncreaseHealth()
    {

        _activeUnit.ResourceContainer.HealthValue.currentValue += 5;
        DisplayStats();
    }

    public void DisplayStats()
    {
        //console log of all relevant stat changes
        float currentHP = _activeUnit.ResourceContainer.HealthValue.currentValue;
        Debug.Log($"Current Health: {currentHP}");
    }
}
