using Assets.DapperEvents.GameEvents;
using Assets.Scripts.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private ActionState currentActionState = ActionState.None;

        private float timeBetweenCharges;
        private float chargeTime;
        private float currentCharge = 0;


        protected Vector2 movementDirection;
        protected Vector2 facingDirection;


        protected virtual void Start()
        {
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
            if (movementDirection.sqrMagnitude > 0.01)
            {
                facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc
            }
        }

        protected virtual void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + movementDirection * speed * Time.fixedDeltaTime);

            //if pressing relevant button queue up a current action and keep useAction running
        }

        public void UnitSelected()
        {
            //target selected on todo
            activeUnitEvent.Raise(this);
        }

        public void UseAction(IAction currentAction)
        {
            //calculate how long the action is charged, and what charge level you reach
            if (currentActionState == ActionState.None)
            {
                //get base charge, max charge level allowed, action size etc from IAction
                //set timebetweencharges to time.deltatime + basecharge; 

                currentActionState = ActionState.Charging;
            }
            else if(currentActionState == ActionState.Charging)
            {
                if(Time.deltaTime >= chargeTime &&  currentAction.ChargeMax() > currentCharge)
                {
                    currentCharge++;
                    timeBetweenCharges *= .8f; //quicker charges as it gets higher
                }
                else if (currentAction.ChargeMax() == currentCharge)
                {
                    //visual and audio effect for max charge
                }
            }
            else if(currentActionState == ActionState.Using)
            {
                //instantiate a new ActiveAction with directionfacing and info from action + chargetime modifiers where applicable
            }

        }
    }

}