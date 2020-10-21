using Assets.Scripts.Actions.Attacks;
using Elebris.Library.Units;
using Elebris.Library.Values;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Base class for all Units, Inhereted by player, character(uncontrolled hero), enemies, NPC
    /// </summary>
    public class Unit : MonoBehaviour
    {

        [SerializeField] protected float speed = 5f;

        public IUnitController _unitController;

        protected Rigidbody2D _rigidbody;
        protected Animator _animator;//make sure the base idle down animation is at the top of the blend tree

        protected ResourceContainer _resourceContainer = new ResourceContainer();
        protected SkillContainer _skillContainer = new SkillContainer();
        protected AttackContainer _attackContainer = new AttackContainer();

        protected ActiveAction currentAction;

        protected ActionScriptableObject _actionPrototype;

        protected GameObject actionObject;

        protected bool lockedByAction = false;

        protected ActionState currentActionState = ActionState.None;

        protected Vector2 movementDirection = new Vector2(0, -1);
        protected Vector2 facingDirection = new Vector2(0, -1);
        public ResourceContainer ResourceContainer { get => _resourceContainer; set => _resourceContainer = value; }
        public SkillContainer SkillContainer { get => _skillContainer; set => _skillContainer = value; }
        public AttackContainer AttackContainer { get => _attackContainer; set => _attackContainer = value; }

        public ValueHolder chargeTime = new ValueHolder(1.5f, 0, StatsEnum.HealthResource);
        public ValueHolder chargeAmount = new ValueHolder(3, 0, StatsEnum.HealthResource);// placeholder Enum. Needs to either be a nullable value or removed from the valueholder and put in a child class


        protected virtual void Start()
        {
            ResetActionState();
            //Set attacks and skills from Saved config
            _unitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            _rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            _animator = GetComponent<Animator>(); //find controller on this character, receives a normalized value
            
        }
        protected virtual void Update()
        {
            if (lockedByAction) return; //this is specifically how long a unit is locked out of ANY movemement or action after using an action. the "vulnerability" window
            //Debug.Log(currentActionState);
            _animator.SetFloat("Horizontal", facingDirection.x);
            _animator.SetFloat("Vertical", facingDirection.y);
            _animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing

            movementDirection = _unitController.ReturnMovement();
            if (movementDirection.sqrMagnitude > 0.01 && currentActionState == ActionState.None)
            {
                facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc

            }
            if (currentActionState == ActionState.None)
            {
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
            //If I break out of an action (cancel, stunned, maneuver etc) then i need to call stopcoroutine on charging
        }

            protected void CheckActionInput()
            {

                if (_unitController.ChargingSelect)
                {
                    //UseAction();
                    //Debug.Log("ChargingSelect");
                }
                else if (_unitController.ChargingManeuver)
                {
                    //Debug.Log("ChargingManeuver");
                }

                else if (_unitController.ChargingLightAttack)
                {
                    //Debug.Log("ChargingLightAttack");
                    UseAction(AttackContainer.LightAttack);
                }
                else if (_unitController.ChargingHeavyAttack)
                {
                    //Debug.Log("ChargingHeavyAttack");
                    UseAction(AttackContainer.HeavyAttack);
                }

                else if (_unitController.ChargingSkillOne)
                {
                    //Debug.Log("ChargingSkillOne");
                    UseAction(SkillContainer.SkillOne);
                }
                else if (_unitController.ChargingSkillTwo)
                {
                    //Debug.Log("ChargingSkillTwo");
                    UseAction(SkillContainer.SkillTwo);
                }
                else if (_unitController.ChargingSkillThree)
                {
                    //Debug.Log("ChargingSkillThree");
                    UseAction(SkillContainer.SkillThree);
                }
                else if (_unitController.ChargingSkillFour)
                {
                    //Debug.Log("ChargingSkillFour");
                    UseAction(SkillContainer.SkillFour);
                }


            }
            protected void CheckActionRelease()
            {
                //only reachable once a skill has been prepared to use, so other inputs should be disabled and this should only fire off at a "safe time"
                //it iS ugly, so I'll need to find a better way to loop the values. I should feel guilty about this but I also have so little experience with the new input system.
                //Debug.Log("Checking release");
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
            if (lockedByAction) return; //this is specifically how long a unit is locked out of ANY movemement or action after using an action. the "vulnerability" window

            float currentSpeed;
            if (currentActionState == ActionState.Charging)
            {
                currentSpeed = speed * .75f;
            }
            else
            {
                currentSpeed = speed;
            }
            // Debug.Log(currentSpeed);
            _rigidbody.MovePosition(_rigidbody.position + movementDirection * currentSpeed * Time.fixedDeltaTime);
        }


        protected void UseAction(ActionScriptableObject actionPrototype)
        {

            //Debug.Log("Use Action");
            if (actionPrototype == null) return;
            //calculate how long the action is charged, and what charge level you reach
            if (currentActionState == ActionState.None)
            {
                _actionPrototype = actionPrototype;
                actionObject = _actionPrototype.actionPrefab;

                chargeTime.MaxValue = actionPrototype.baseChargeTime;
                chargeAmount.MaxValue = actionPrototype.chargeMax;
                chargeAmount.CurrentValue = -1;
                SetNextChargeTime();

                currentActionState = ActionState.Charging;
            }
        }

        protected void ChargeAction()
        {
            if (chargeAmount.MaxValue == chargeAmount.CurrentValue)
            {
                //visual and audio effect for max charge
            }
        }

        protected void SpawnAction()
        {
            _animator.SetTrigger("ActionUsed");
            StartCoroutine(LockDuringAction(_actionPrototype.animationLength)); //time spent "casting" the skill (locked in place) whereas action duration is how long it sticks around
            // I really wanted a way to create a clone of a gameobject without cloning it, tweaking values, and then instantiating it, but that currently doesnt seem easy/possib le
            Vector3 actionDirection = new Vector3(facingDirection.x, facingDirection.y, 0);

            GameObject spawn = Instantiate(actionObject, transform.position + actionDirection, Quaternion.identity);
            currentAction = spawn.GetComponent<ActiveAction>(); //update the Active Action attached to the new prefab
            currentAction.Initialize(this, (int)chargeAmount.CurrentValue, _actionPrototype.actionDuration, facingDirection, _actionPrototype.actionSpeed);
            ResetActionState();
        }

        protected void ResetActionState()
        {
            //after casting reset state.
            currentActionState = ActionState.None;
            actionObject = null;
            _actionPrototype = null;
        }

        protected IEnumerator LockDuringAction(float time)
        {
            lockedByAction = true;
            //activate "action" trigger to change from movement/charging or attacking animation
            yield return new WaitForSeconds(time);
            lockedByAction = false;
        }

        protected void SetNextChargeTime()
        {
            chargeAmount.CurrentValue++;

            chargeTime.MaxValue *= .8f; //quicker charges as it gets higher
            StartCoroutine(CheckChargeAction(chargeTime.MaxValue));
            //Debug.Log($"{chargeTime.MaxValue} max time, {chargeTime.CurrentValue} current time  and then abse charge time{_actionPrototype.baseChargeTime}");

        }

        protected IEnumerator CheckChargeAction(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (currentActionState == ActionState.Charging && chargeAmount.CurrentValue < chargeAmount.MaxValue)
            {
                SetNextChargeTime();
            }
        }
    }

}