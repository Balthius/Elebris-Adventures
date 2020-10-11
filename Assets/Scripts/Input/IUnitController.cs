using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Any Unit needs to implement this class separately for their relavant movement and action types,
/// and you should only be calling these methods to move, attack etc with units
/// </summary>
namespace Assets.Scripts.Units
{

    public interface IUnitController
    {
        Vector2 ReturnMovement();

        void EnableActionInputs();

        //return true if cooresponding button pressed pressed
        InputActionPhase LightAttack
        {
            get;
        }
        InputActionPhase HeavyAttack
        {
            get;
        }
        InputActionPhase SkillOne
        {
            get;
        }
        InputActionPhase SkillTwo
        {
            get;
        }
        InputActionPhase SkillThree
        {
            get;
        }
        InputActionPhase SkillFour
        {
            get;
        }
        InputActionPhase Maneuver
        {
            get;
        }
        InputActionPhase Select
        {
            get;
        }

    }

}