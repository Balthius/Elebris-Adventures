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

    [RequireComponent(typeof(Hero))]
    public class PlayerInputController : MonoBehaviour, IUnitController
    {

        private Vector2 currentMovement;

        List<InputAction> interactionInputs = new List<InputAction>();

        InputAction activeAction;

        private InputActions _inputActions; //simple version for single player on system, not splitscreen

        public Vector2 CurrentMovement { get => _inputActions.Gameplay.Movement.ReadValue<Vector2>(); set => currentMovement = value; }

        //could alternatively use _inputActions.Gameplay.LightAttack.triggered and return a bool; or inputAction.ReadValue<float>() > 0

        //input logic needs to be kept completely on this side so I can't pass GameplayActions through




        /// <summary>
        /// TODO 
        /// need to return a button pressed = true and a button released = false; 
        /// that is ALL. true, and false. charging or not.
        /// no more BS trying to bring over input itself
        /// </summary>
        public bool ChargingLightAttack
        {
            get
            {
                if (_inputActions.Gameplay.LightAttack.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.LightAttack);
                }
                //Debug.Log($"light attack value{_inputActions.Gameplay.LightAttack.ReadValue<float>()}");
                return _inputActions.Gameplay.LightAttack.ReadValue<float>() > 0;
            }
        }

        public bool ChargingHeavyAttack
        {
            get
            {
                if (_inputActions.Gameplay.HeavyAttack.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.HeavyAttack);
                }

                return _inputActions.Gameplay.HeavyAttack.ReadValue<float>() > 0;
            }
        }
        public bool ChargingSkillOne
        {
            get
            {
                if (_inputActions.Gameplay.SkillOne.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillOne);
                }

                return _inputActions.Gameplay.SkillOne.ReadValue<float>() > 0;
            }
        }

        public bool ChargingSkillTwo
        {
            get
            {
                if (_inputActions.Gameplay.SkillTwo.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillTwo);
                }

                return _inputActions.Gameplay.SkillTwo.ReadValue<float>() > 0;
            }
        }

        public bool ChargingSkillThree
        {
            get
            {
                if (_inputActions.Gameplay.SkillThree.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillThree);
                }

                return _inputActions.Gameplay.SkillThree.ReadValue<float>() > 0;
            }
        }

        public bool ChargingSkillFour
        {
            get
            {
                if (_inputActions.Gameplay.SkillFour.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.SkillFour);
                }

                return _inputActions.Gameplay.SkillFour.ReadValue<float>() > 0;
            }
        }

        public bool ChargingManeuver
        {
            get
            {
                if (_inputActions.Gameplay.Maneuver.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.Maneuver);
                }

                return _inputActions.Gameplay.Maneuver.ReadValue<float>() > 0;
            }
        }
        public bool ChargingSelect
        {
            get
            {
                //added for posterity, but might be useful for things like "collect all loot"
                if (_inputActions.Gameplay.Select.phase == InputActionPhase.Started)
                {
                    LockOtherInputs(_inputActions.Gameplay.Select);
                }

                return _inputActions.Gameplay.Select.ReadValue<float>() > 0;
            }
        }

        private void Awake()
        {
            _inputActions = new InputActions();
        }
        private void FixedUpdate()
        {
            //Debug.Log($"Active action is: {activeAction} and value is {activeAction.ReadValue<float>()}");
            if (activeAction != null && activeAction.ReadValue<float>() < 0.1)
            {
                //Debug.Log($"unlock inputs");
                EnableActionInputs();
            }
        }
        private void OnEnable()
        {
            _inputActions.Gameplay.Movement.Enable();
            _inputActions.Gameplay.Select.Enable();
            FillList();
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
        public void FillList()
        {
            //remove Maneuver, and then use Maneuver to cancel attacks(and set activeAction.Disable();)

            interactionInputs.Add(_inputActions.Gameplay.LightAttack);
            interactionInputs.Add(_inputActions.Gameplay.HeavyAttack);
            interactionInputs.Add(_inputActions.Gameplay.Maneuver);
            interactionInputs.Add(_inputActions.Gameplay.Select);
            interactionInputs.Add(_inputActions.Gameplay.SkillOne);
            interactionInputs.Add(_inputActions.Gameplay.SkillTwo);
            interactionInputs.Add(_inputActions.Gameplay.SkillThree);
            interactionInputs.Add(_inputActions.Gameplay.SkillFour);

        }
        public void EnableActionInputs()
        {
            _inputActions.Gameplay.Select.Enable();
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
            activeAction = inputAction;

            //Debug.Log($"lock inputs other than {activeAction.name}");
            foreach (var action in interactionInputs)
            {
                if (action != inputAction)
                {
                    action.Disable();
                }
            }
        }

    }
}

//instead of enabling and disabling every action I could set up an enum of ready, charging, locked and toggle each value that way.
//TODO: add class to let the player know when certain buttons are pressed (for on screen button indicators). Not connected to this class at all, but shares InputActions.cs to receive events 