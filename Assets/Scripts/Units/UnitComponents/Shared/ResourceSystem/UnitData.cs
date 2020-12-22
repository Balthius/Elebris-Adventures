using Assets.Scripts.Units;
using Elebris.Rpg.Library.CharacterValues;
using Elebris.Rpg.Library.StatGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public CharacterValueContainer ValueContainer { get; set; } 
    public CharacterActionContainer ActionContainer { get; set; }

    [SerializeField] private Guid unitGuid;
    private void Awake()
    {     
        if(unitGuid != null)
        {
            ValueContainer = CharacterGenerationHandler.CreateCharacterValues(unitGuid);
        }   
        else
        {
            ValueContainer = CharacterGenerationHandler.CreateCharacterValues();
        }

        ActionContainer = gameObject.AddComponent<CharacterActionContainer>(); //same as above, later
        //generate or find classes from guid
        //generate or find attributes from guid
        //generate or find stats from guid
        //generate or find inventories from guid, including whats on the hotbar

    }


}
