﻿using UnityEngine;
/// <summary>
/// Any Unit needs to implement this class separately for their relavant movement and action types,
/// and you should only be calling these methods to move, attack etc with units
/// </summary>
namespace Assets.Scripts.Units
{

    public interface IUnitController
    {
        Vector2 ReturnMovement();


        //return true if corresponding button pressed pressed
        bool ChargingLightAttack
        {
            get;
        }
        bool ChargingHeavyAttack
        {
            get;
        }
        bool ChargingSkillOne
        {
            get;
        }
        bool ChargingSkillTwo
        {
            get;
        }
        bool ChargingSkillThree
        {
            get;
        }
        bool ChargingSkillFour
        {
            get;
        }
        bool ChargingManeuver
        {
            //Also cancels other actions when used
            get;
        }
        bool ChargingSelect
        {
            get;
        }

    }

}