using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://gamedev.stackexchange.com/questions/46819/making-characters-skills-and-abilities-as-commands-good-practice
/// </summary>
public class ActionBuilder : ScriptableObject, IActionBehaviour
{
    public GameObject obj;
    public Sprite ActionIcon;

    public float baseChargeTime;
    public float actionLock;
    public float actionDuration;
    public float actionSpeed;

    public float actionCooldown;
    public bool canCharge;
    public bool destroyOnContact; //some skills will stay their whole duration, or pierce etc
    public object ReturnBehaviour()
    {
        return this;
    }
    ResourceCost[] ResourceCosts;
    // Identification and information
    public string displayName; // Name used for display purposes for the GUI
                               // We don't need an identifier field, because this will actually be stored
                               // as a file on disk and thus implicitly have its own identifier string.

    // Description of damage to targets

    // I put this enum inside the class for answer readability, but it really belongs outside, inside a namespace rather than nested inside a class


    
    public float duration; // Used for over-time type damages, or as a delay for insta-hit damage

    // Visual FX}
    public TwinStickEffectPlacement placement;

    [Serializable]
    public class AbilityEffects
    {
        public ActionEffectBehaviour actionEffect;
    }

    public AbilityEffects[] abilityEffects;

}
