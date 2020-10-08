using System;
using UnityEngine;
/// <summary>
/// Any Unit needs to implement this class separately for their relavant movement and action types,
/// and you should only be calling these methods to move, attack etc with units
/// </summary>
namespace Assets.Scripts.Units
{ 

    public interface IUnitController
    {
        Vector2 ReturnMovement();
    }
}