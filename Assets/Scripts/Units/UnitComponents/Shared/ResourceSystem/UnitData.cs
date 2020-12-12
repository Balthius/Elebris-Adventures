using Assets.Scripts.Units;
using Elebris.Rpg.Library.CharacterValues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public CharacterValueContainer ValueContainer { get; set; } = new CharacterValueContainer();
    public CharacterActionContainer ActionContainer { get; set; } = new CharacterActionContainer();


    private void Awake()
    {
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
