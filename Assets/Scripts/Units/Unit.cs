using Assets.DapperEvents.GameEvents;
using Assets.Scripts.Actions;
using Assets.Scripts.Actions.Attacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Units
{

    /// <summary>
    /// Base class for all Units, Inhereted by player, character(uncontrolled hero), enemies, NPC
    /// </summary>

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Unit : MonoBehaviour
    {

        [SerializeField] private ActiveUnitEvent activeUnitEvent = null;
        //we have the option of changing player scale to -1 when moving left and  1 when moving right in order to have a "player side" sprite transform.localScale
        //rather than a left and right, that can be reused. I'm not currently going this route because I want left and right assets to be more than just mirror copies of eachother
        protected IUnitController _unitController;
        protected Rigidbody2D _rigidbody;
        protected Animator _animator;//make sure the base idle down animation is at the top of the blend tree
        private ResourceContainer _resourceContainer = new ResourceContainer();
        private SkillContainer _skillContainer = new SkillContainer();
        private AttackContainer _attackContainer = new AttackContainer();
        public ResourceContainer ResourceContainer { get => _resourceContainer; set => _resourceContainer = value; }
        public SkillContainer SkillContainer { get => _skillContainer; set => _skillContainer = value; }
        public AttackContainer AttackContainer { get => _attackContainer; set => _attackContainer = value; }

        [SerializeField] protected float speed = 5f;


        protected Vector2 movementDirection;
        protected Vector2 facingDirection;

        private float timeBetweenCharges;
        private float chargeTime;

        private ActionState currentActionState = ActionState.None;

        private ActiveAction currentAction;

        GameObject actionObject;


        protected virtual void Start()
        {
            //Set attacks and skills from Saved config
            _unitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            _rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            _animator = GetComponent<Animator>(); //find controller on this character, receives a normalized value
            UnitSelected(); //select this unit on start
        }
        protected virtual void Update()
        {
            _animator.SetFloat("Horizontal", facingDirection.x);
            _animator.SetFloat("Vertical", facingDirection.y);
            _animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing

            movementDirection = _unitController.ReturnMovement();
            if (movementDirection.sqrMagnitude > 0.01 && currentActionState == ActionState.None)
            {
                facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc
            }

            if(currentAction != null)
            {
                ChargeAction();
                if (currentActionState != ActionState.Using) return;
                CheckActionRelease();
            }
            else
            {
                CheckActionInput();
            }
        }

        private void CheckActionInput()
        {
            if (_unitController.Select == InputActionPhase.Started)
            {
                //UseAction();
            }
            else if (_unitController.LightAttack == InputActionPhase.Started)
            {
                UseAction(AttackContainer.LightAttack);
            }
            else if (_unitController.HeavyAttack == InputActionPhase.Started)
            {
                UseAction(AttackContainer.HeavyAttack);
            }
            else if (_unitController.Maneuver == InputActionPhase.Started)
            {

            }
            else if (_unitController.SkillOne == InputActionPhase.Started)
            {
                UseAction(SkillContainer.SkillOne);
            }
            else if (_unitController.SkillTwo == InputActionPhase.Started)
            {
                UseAction(SkillContainer.SkillTwo);
            }
            else if (_unitController.SkillThree == InputActionPhase.Started)
            {
                UseAction(SkillContainer.SkillThree);
            }
            else if (_unitController.SkillFour == InputActionPhase.Started)
            {
                UseAction(SkillContainer.SkillFour);
            }


        }
        private void CheckActionRelease()
        {
            if (_unitController.Select == InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.LightAttack != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.HeavyAttack != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.Maneuver != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.SkillOne != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.SkillTwo != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.SkillThree != InputActionPhase.Started)
            {
                currentActionState = ActionState.Using;
            }
            else if (_unitController.SkillFour != InputActionPhase.Canceled)
            {
                currentActionState = ActionState.Using;
            }
        }

        protected virtual void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + movementDirection * speed * Time.fixedDeltaTime);
        }

        public void UnitSelected()
        {
            //targetselectedevent on todo list
            activeUnitEvent.Raise(this);
        }

        public void UseAction(ActionScriptableObject actionPrototype)
        {
            if (actionPrototype == null)
            {
                Debug.Log($"No prototype");
                return;
            }
            //calculate how long the action is charged, and what charge level you reach
            if (currentActionState == ActionState.None)
            {
                Debug.Log("New action started");
                actionObject = actionPrototype.actionPrefab;
                currentAction.Initialize(this, actionPrototype.actionDuration, actionPrototype.chargeMax, facingDirection);

                timeBetweenCharges = actionPrototype.baseChargeTime;
                SetNextChargeTime();

                currentActionState = ActionState.Charging;
            }
        }

        private void ChargeAction()
        {
            if (currentActionState == ActionState.Charging)
            {
                if (Time.deltaTime >= chargeTime && currentAction.maxCharges > currentAction.currentCharge)
                {
                    Debug.Log($"Action charging{ currentAction.currentCharge}");

                    SetNextChargeTime();
                }
                else if (currentAction.maxCharges == currentAction.currentCharge)
                {
                    //visual and audio effect for max charge
                }
            }
            else if (currentActionState == ActionState.Using)
            {
                // I really wanted a way to create a clone of a gameobject without cloning it, tweaking values, and then instantiating it, but that currently doesnt seem easy/possib le
                GameObject spawn = Instantiate(actionObject);
                ActiveAction spawnAction = actionObject.GetComponent<ActiveAction>();
                spawnAction = currentAction; // overwrite objects action
                //after casting reset state.
                currentActionState = ActionState.None;
            }
        }

        private void SetNextChargeTime()
        {
            currentAction.currentCharge++;
            chargeTime = Time.deltaTime + timeBetweenCharges; // set, and then reduce next required charge time
            timeBetweenCharges *= .8f; //quicker charges as it gets higher
        }

     
    }

}