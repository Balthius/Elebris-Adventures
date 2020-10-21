using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentPanel : MonoBehaviour
{
    Hero _activeUnit;

    

    [SerializeField] ActionContainerScriptableObject container;
    public void Initialize(Hero activeUnit)
    {
        _activeUnit = activeUnit;
        TestSkillSet(); // remove this reference later and just use the one on the test panel
    }

    public void DecreaseHealth()
    {

        //these can be moved into a "changehealth" method with a parameter of int/float in order to crunch this down to 1 method
        _activeUnit.ResourceContainer.HealthValue.CurrentValue -= 5;
        DisplayStats();
    }
    public void IncreaseHealth()
    {

        _activeUnit.ResourceContainer.HealthValue.CurrentValue += 5;
        DisplayStats();
    }

    public void DisplayStats()
    {
        //console log of all relevant stat changes
        float currentHP = _activeUnit.ResourceContainer.HealthValue.CurrentValue;
        Debug.Log($"Current Health: {currentHP}");
    }

    public void TestSkillSet()
    {
        _activeUnit.AttackContainer.LightAttack = container.lightAttack;
        _activeUnit.AttackContainer.HeavyAttack = container.heavyAttack;
        _activeUnit.SkillContainer.SkillOne = container.skillOne;
        _activeUnit.SkillContainer.SkillTwo = container.skillTwo;
        _activeUnit.SkillContainer.SkillThree = container.skillThree;
        _activeUnit.SkillContainer.SkillFour = container.skillFour;
    }
}
