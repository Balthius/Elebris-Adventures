using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// This class is responsible for passing Input from the player to the Active character class. IUnitController is the only public portion of 
/// this class so you should only call this via the interface
/// Other classes to control AI input need to implement the IUnitcontroller separately
/// </summary>

namespace Assets.Scripts.Units
{
    [RequireComponent(typeof(Character))]
    public class PlayerInputController : MonoBehaviour, IUnitController
    {

        private Vector2 currentMovement;
      
        private InputActions _inputActions; //simple version for single player on system, not splitscreen

        public Vector2 CurrentMovement { get => _inputActions.Gameplay.Movement.ReadValue<Vector2>(); set => currentMovement = value; }

        private void Awake()
        {
            _inputActions = new InputActions();
        }
       
        private void OnEnable()
        {
            
            _inputActions.Gameplay.Movement.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Gameplay.Movement.Disable();
        }

        public Vector2 ReturnMovement()
        {
            return CurrentMovement;
        }
    }
}