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
    [RequireComponent(typeof(Unit))]
    public class PlayerInputController : MonoBehaviour, IUnitController
    {

        private Vector2 currentMovement;

        List<InputAction> interactionInputs = new List<InputAction>();

        InputAction activeAction;
      
        private InputActions _inputActions; //simple version for single player on system, not splitscreen

        public Vector2 CurrentMovement { get => _inputActions.Gameplay.Movement.ReadValue<Vector2>(); set => currentMovement = value; }

        //could alternatively use _inputActions.Gameplay.LightAttack.triggered and return a bool; or inputAction.ReadValue<float>() > 0

        public InputActionPhase LightAttack
        {
            get
            {
                if(_inputActions.Gameplay.LightAttack.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.LightAttack);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        public InputActionPhase HeavyAttack
        {
            get
            {
                if (_inputActions.Gameplay.HeavyAttack.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.HeavyAttack);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }
        public InputActionPhase SkillOne
        {
            get
            {
                if (_inputActions.Gameplay.SkillOne.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillOne);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        public InputActionPhase SkillTwo
        {
            get
            {
                if (_inputActions.Gameplay.SkillTwo.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillTwo);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        public InputActionPhase SkillThree
        {
            get
            {
                if (_inputActions.Gameplay.SkillThree.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillThree);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        public InputActionPhase SkillFour
        {
            get
            {
                if (_inputActions.Gameplay.SkillFour.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillFour);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        public InputActionPhase Maneuver
        {
            get
            {
                if (_inputActions.Gameplay.Maneuver.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.Maneuver);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }
        public InputActionPhase Select
        {
            get
            {
                if (_inputActions.Gameplay.Select.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.Select);
                }
                return _inputActions.Gameplay.LightAttack.phase;
            }
        }

        private void Awake()
        {
            _inputActions = new InputActions();
        }
        private void FixedUpdate()
        {
            if (activeAction != null && activeAction.phase != InputActionPhase.Started)
            {
                Debug.Log("Unlock other inputs");
                EnableActionInputs();
            }
        }
        private void OnEnable()
        {
            _inputActions.Gameplay.Movement.Enable();
            _inputActions.Gameplay.Select.Enable();
            EnableActionInputs();
        }

        private void OnDisable()
        {
            interactionInputs = null;
            _inputActions.Gameplay.Movement.Disable();
            _inputActions.Gameplay.LightAttack.Disable();
            _inputActions.Gameplay.HeavyAttack.Disable();
            _inputActions.Gameplay.Select.Disable();
            _inputActions.Gameplay.Maneuver.Disable();
            _inputActions.Gameplay.SkillOne.Disable();
            _inputActions.Gameplay.SkillTwo.Disable();
            _inputActions.Gameplay.SkillThree.Disable();
            _inputActions.Gameplay.SkillFour.Disable();
        }

        public Vector2 ReturnMovement()
        {
            return CurrentMovement;
        }

        public void EnableActionInputs()
        {
            interactionInputs.Add(_inputActions.Gameplay.LightAttack);
            interactionInputs.Add(_inputActions.Gameplay.HeavyAttack);
            interactionInputs.Add(_inputActions.Gameplay.Maneuver);
            interactionInputs.Add(_inputActions.Gameplay.Select);
            interactionInputs.Add(_inputActions.Gameplay.SkillOne);
            interactionInputs.Add(_inputActions.Gameplay.SkillTwo);
            interactionInputs.Add(_inputActions.Gameplay.SkillThree);
            interactionInputs.Add(_inputActions.Gameplay.SkillFour);

            _inputActions.Gameplay.LightAttack.Enable();
            _inputActions.Gameplay.HeavyAttack.Enable();
            _inputActions.Gameplay.Maneuver.Enable();
            _inputActions.Gameplay.SkillOne.Enable();
            _inputActions.Gameplay.SkillTwo.Enable();
            _inputActions.Gameplay.SkillThree.Enable();
            _inputActions.Gameplay.SkillFour.Enable();
            activeAction = null;
        }

        void LockOtherInputs(InputAction inputAction)
        {
            if (activeAction != null) return;
            Debug.Log("Lock other inputs");
            activeAction = inputAction;
            foreach (var action in interactionInputs)
            {
                if(action != inputAction)
                {
                    action.Disable();
                }
            }
        }

    }
}
//TODO: add class to let the player know when certain buttons are pressed (for on screen button indicators). Not connected to this class at all, but shares InputActions.cs to receive events 