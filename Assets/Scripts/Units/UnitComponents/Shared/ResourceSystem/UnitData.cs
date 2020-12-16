using Assets.Scripts.Units;
using Elebris.Rpg.Library.CharacterValues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public CharacterValueContainer ValueContainer { get; set; } 
    public CharacterActionContainer ActionContainer { get; set; }


    private void Awake()
    {
        ValueContainer = new CharacterValueContainer();
        ActionContainer = gameObject.AddComponent<CharacterActionContainer>();
        //generate or find classes from guid
        //generate or find attributes from guid
        //generate or find stats from guid
        //generate or find inventories from guid, including whats on the hotbar

    }



    private void OnEnable()
    {
        //subscribe resourcebars to events when their max stats change
    }
    private void OnDisable()
    {
        //unsubscribe resourcebars to events when their max stats change
    }
}
