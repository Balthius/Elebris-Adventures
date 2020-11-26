using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Input;
using Elebris.Core.Library.Components;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Base class for all Units, Inhereted by player, character(uncontrolled hero), enemies, NPC
    /// </summary>
    public class Unit : MonoBehaviour
    {
        #region Core Containers and stats
        [SerializeField] protected float speed = 5f;
        protected float currentSpeed;
        protected Vector2 movementDirection = new Vector2(0, 0);
        protected Vector2 facingDirection = new Vector2(0, -1);

        public Character Character { get; set; }


        //public ActionContainerScriptableObject defaultActions;

        public AttackContainer AttackContainer { get; set; }
        public SkillContainer SkillContainer { get; set; }

        public PassiveContainer PassiveContainer { get; set; }
        public IUnitController UnitController { get => unitController; set => unitController = value; }
        public Rigidbody2D Rigidbody { get => rigidBody; set => rigidBody = value; }
        public Animator Animator { get => animator; set => animator = value; }

        #endregion

        #region Components

        protected IUnitController unitController;
        protected Rigidbody2D rigidBody;
        protected Animator animator;//make sure the base idle down animation is at the top of the blend tree


        #endregion
        #region UnityCallbacks
        private void Awake()
        {
            UnitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            Rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            Animator = GetComponentInChildren<Animator>(); //find controller on this character, receives a normalized value

        }
        protected virtual void Start()
        {
            PassiveContainer.ModifyCharacter(this);
            //Set attacks and skills from Saved config
            //AttackContainer.LightAttack = defaultActions.lightAttack;
            //AttackContainer.HeavyAttack = defaultActions.heavyAttack;
            //SkillContainer.SkillOne = defaultActions.skillOne;
            //SkillContainer.SkillTwo = defaultActions.skillTwo;
            //SkillContainer.SkillThree = defaultActions.skillThree;
            //SkillContainer.SkillFour = defaultActions.skillFour;

            ChangeState(new ActionWaiting());
            currentSpeed = speed;

        }
        protected virtual void Update()
        {
            if (lockedByAction) return; //this is specifically how long a unit is locked out of ANY movemement or action after using an action. the "vulnerability" window
            //Debug.Log(currentActionState);
            Animator.SetFloat("Horizontal", facingDirection.x);
            Animator.SetFloat("Vertical", facingDirection.y);
            Animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing
            //Debug.Log($"the values {gameObject.name} is using to move are {_unitController.ReturnMovement().normalized}");
            movementDirection = UnitController.ReturnMovement().normalized;
            if (movementDirection.sqrMagnitude > 0.01 && canChangeFacing)
            {
                facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc

            }


            //If I break out of an action (cancel, stunned, maneuver etc) then i need to call stopcoroutine on charging
        }


        protected virtual void FixedUpdate()
        {
            if (lockedByAction) return; //this is specifically how long a unit is locked out of ANY movemement or action after using an action. the "vulnerability" window

            if (movementDirection.sqrMagnitude > 0.01)
            {
                Debug.Log($"Can move Unit {currentSpeed} and {movementDirection}");
                Rigidbody.MovePosition(Rigidbody.position + movementDirection * currentSpeed * Time.fixedDeltaTime);
            }
        }

        public void SetSpeed(float modifier)
        {
            //remove this once speed is controlled as a stat and can be affected through those channels
            currentSpeed = speed * modifier;
        }

        #endregion


        #region Action

        protected bool lockedByAction = false;
        public IChargingState currentChargeState;
        public ActionBase ActionBase { get; set; }


        public bool canChangeFacing = true;


        public bool UseAction(BoundAction action)
        {
            if (ActionBase != null) return false;
            ActionBase = new ActionBase();
            ActionBase.ActionInfo = action.ActionInfo;
            ActionBase.ActionPacket = action.ActionPacket;
            return true;
        }


        public void SpawnAction(int charge)
        {
            PassiveContainer.ModifyAction(ActionBase, this);
            StartCoroutine(LockDuringAction(ActionBase.ActionInfo.animationLength.ReturnValue)); //time spent "casting" the skill (locked in place) whereas action duration is how long it sticks around
            // I really wanted a way to create a clone of a gameobject without cloning it, tweaking values, and then instantiating it, but that currently doesnt seem easy/possib le
            Vector3 actionDirection = new Vector3(facingDirection.x, facingDirection.y, 0);

            GameObject spawn = Instantiate(ActionBase.ActionInfo.actionPrefab, transform.position + actionDirection, Quaternion.identity);
            ExecutedAction executedAction = spawn.GetComponent<ExecutedAction>(); 
            executedAction.Initialize(this, charge, facingDirection, ActionBase);

        }

        public IEnumerator LockDuringAction(float time)
        {
            lockedByAction = true;
            //activate "action" trigger to change from movement/charging or attacking animation
            yield return new WaitForSeconds(time);
            lockedByAction = false;
        }



        public void ChangeState(IChargingState newState)
        {
            if (currentChargeState != null)
            {
                currentChargeState.Exit();
            }
            currentChargeState = newState;
            currentChargeState.Enter(this);
        }
        #endregion

    }

}