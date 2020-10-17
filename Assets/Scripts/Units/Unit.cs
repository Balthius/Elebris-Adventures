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
        public IUnitController _unitController;
        protected Rigidbody2D _rigidbody;
        protected Animator _animator;//make sure the base idle down animation is at the top of the blend tree
        private ResourceContainer _resourceContainer = new ResourceContainer();
        private SkillContainer _skillContainer = new SkillContainer();
        private AttackContainer _attackContainer = new AttackContainer();
        public ResourceContainer ResourceContainer { get => _resourceContainer; set => _resourceContainer = value; }
        public SkillContainer SkillContainer { get => _skillContainer; set => _skillContainer = value; }
        public AttackContainer AttackContainer { get => _attackContainer; set => _attackContainer = value; }

        [SerializeField] protected float speed = 5f;

        protected Vector2 movementDirection = new Vector2(0, -1);
        protected Vector2 facingDirection = new Vector2(0, -1);
        //add a "run" function similar to charging where as long as your moving you start to fill a hidden bar, when it fills you boost your movespeed by 1.25

        private float timeBetweenCharges;
        private float chargeTime;
        private int currentCharge = 0, chargeMax = 3;

        private ActionState currentActionState = ActionState.None;

        private ActiveAction currentAction;

        private ActionScriptableObject _actionPrototype;

        private bool chargingAction;

        GameObject actionObject;


        protected virtual void Start()
        {
            ResetActionState();
            //Set attacks and skills from Saved config
            _unitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            _rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            _animator = GetComponent<Animator>(); //find controller on this character, receives a normalized value
            UnitSelected(); //select this unit on start
        }
        protected virtual void Update()
        {
            Debug.Log(currentActionState);
            _animator.SetFloat("Horizontal", facingDirection.x);
            _animator.SetFloat("Vertical", facingDirection.y);
            _animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing

            movementDirection = _unitController.ReturnMovement();
            if (movementDirection.sqrMagnitude > 0.01 && currentActionState == ActionState.None)
            {
                facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc
                CheckActionInput();
            }
            if (currentActionState == ActionState.Charging)
            {
                _animator.SetBool("Charging", true);
                ChargeAction();

                CheckActionRelease();
            }
            if (currentActionState == ActionState.Using)
            {
                SpawnAction();

                _animator.SetBool("Charging", false);
            }

        }

        private void CheckActionInput()
        {
            
            if (_unitController.ChargingSelect)
            {
                //UseAction();
                Debug.Log("ChargingSelect");
            }
            else if (_unitController.ChargingManeuver)
            {
                Debug.Log("ChargingManeuver");
            }

            else if (_unitController.ChargingLightAttack)
            {
                Debug.Log("ChargingLightAttack");
                UseAction(AttackContainer.LightAttack);
            }
            else if (_unitController.ChargingHeavyAttack)
            {
                Debug.Log("ChargingHeavyAttack");
                UseAction(AttackContainer.HeavyAttack);
            }

            else if (_unitController.ChargingSkillOne)
            {
                Debug.Log("ChargingSkillOne");
                UseAction(SkillContainer.SkillOne);
            }
            else if (_unitController.ChargingSkillTwo)
            {
                Debug.Log("ChargingSkillTwo");
                UseAction(SkillContainer.SkillTwo);
            }
            else if (_unitController.ChargingSkillThree)
            {
                Debug.Log("ChargingSkillThree");
                UseAction(SkillContainer.SkillThree);
            }
            else if (_unitController.ChargingSkillFour)
            {
                Debug.Log("ChargingSkillFour");
                UseAction(SkillContainer.SkillFour);
            }


        }
        private void CheckActionRelease()
        {
            //only reachable once a skill has been prepared to use, so other inputs should be disabled and this should only fire off at a "safe time"
            //it iS ugly, so I'll need to find a better way to loop the values. I should feel guilty about this but I also have so little experience with the new input system.
            Debug.Log("Checking release");
            if (!_unitController.ChargingSelect &&
                !_unitController.ChargingLightAttack &&
                !_unitController.ChargingHeavyAttack &&
                !_unitController.ChargingManeuver &&
                !_unitController.ChargingSkillOne &&
                !_unitController.ChargingSkillTwo &&
                !_unitController.ChargingSkillThree &&
                !_unitController.ChargingSkillFour
                )
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

            Debug.Log("Use Action");
            if (actionPrototype == null) return;
            //calculate how long the action is charged, and what charge level you reach
            if (currentActionState == ActionState.None)
            {
                currentCharge = 0;
                _actionPrototype = actionPrototype;
                Debug.Log("New action started");
                actionObject = _actionPrototype.actionPrefab;

                timeBetweenCharges = actionPrototype.baseChargeTime;
                SetNextChargeTime();

                currentActionState = ActionState.Charging;
            }
        }

        private void ChargeAction()
        {

            Debug.Log("Charge Action");
            if (Time.deltaTime >= chargeTime && chargeMax > currentCharge)
            {
                SetNextChargeTime();
                Debug.Log($"Action charge{ currentCharge}");

            }
            else if (chargeMax == currentCharge)
            {
                //visual and audio effect for max charge
            }
            
          
        }

        private void SpawnAction()
        {
            Debug.Log("Spawn Action");
            // I really wanted a way to create a clone of a gameobject without cloning it, tweaking values, and then instantiating it, but that currently doesnt seem easy/possib le
            Vector3 actionDirection = new Vector3(facingDirection.x, facingDirection.y, 0);


            GameObject spawn = Instantiate(actionObject, transform.position + actionDirection, Quaternion.identity);
            currentAction = spawn.GetComponent<ActiveAction>(); //update the Active Action attached to the new prefab
            currentAction.Initialize(this, currentCharge, _actionPrototype.actionDuration, facingDirection);
            ResetActionState();
            
        }

        private void ResetActionState()
        {
            //after casting reset state.
            currentActionState = ActionState.None;
            actionObject = null;
            _actionPrototype = null;
        }

        private void SetNextChargeTime()
        {
            currentCharge++;
            chargeTime = Time.deltaTime + timeBetweenCharges; // set, and then reduce next required charge time
            timeBetweenCharges *= .8f; //quicker charges as it gets higher
        }

     
    }

}