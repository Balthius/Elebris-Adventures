using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Input;
using Elebris.Library.Enums;
using Elebris.Library.Units;
using Elebris.Library.Units.Containers;
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

        protected ResourceContainer _resourceContainer = new ResourceContainer();
        protected SkillContainer _skillContainer = new SkillContainer();
        protected AttackContainer _attackContainer = new AttackContainer();
        public ResourceContainer ResourceContainer { get => _resourceContainer; set => _resourceContainer = value; }
        public SkillContainer SkillContainer { get => _skillContainer; set => _skillContainer = value; }
        public AttackContainer AttackContainer { get => _attackContainer; set => _attackContainer = value; }
        #endregion

        #region Components

        public IUnitController _unitController;
        protected Rigidbody2D _rigidbody;
        public Animator _animator;//make sure the base idle down animation is at the top of the blend tree


        #endregion

        #region Action
        //Most of these can be separated into a 3 part FSM like ai\s idle,follow,attack fsm

        protected bool lockedByAction = false;
        public IChargingState currentChargeState;

        public ActionScriptableObject _actionPrototype;
        public ActiveAction currentAction;
        protected GameObject actionObject;

        public bool canChangeFacing = true;
      

        public bool UseAction(ActionScriptableObject actionPrototype)
        {
            //Debug.Log("Use Action");
            if (actionPrototype == null) return false;
            //calculate how long the action is charged, and what charge level you reach
            
            _actionPrototype = actionPrototype;
            actionObject = _actionPrototype.actionPrefab;

            return true;
        }


        public void SpawnAction(int charge)
        {
            _animator.SetTrigger("ActionUsed");
            StartCoroutine(LockDuringAction(_actionPrototype.animationLength)); //time spent "casting" the skill (locked in place) whereas action duration is how long it sticks around
            // I really wanted a way to create a clone of a gameobject without cloning it, tweaking values, and then instantiating it, but that currently doesnt seem easy/possib le
            Vector3 actionDirection = new Vector3(facingDirection.x, facingDirection.y, 0);

            GameObject spawn = Instantiate(actionObject, transform.position + actionDirection, Quaternion.identity);
            currentAction = spawn.GetComponent<ActiveAction>(); //update the Active Action attached to the new prefab
            currentAction.Initialize(this, charge, _actionPrototype.actionDuration, facingDirection, _actionPrototype.actionSpeed);
            
        }

        public void ResetActionState()
        {
            actionObject = null;
            _actionPrototype = null;
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



        #region UnityCallbacks
        private void Awake()
        {
            ChangeState(new ActionWaiting());
            TestSkillSet();
        }
        protected virtual void Start()
        {
            ResetActionState();
            //Set attacks and skills from Saved config
            _unitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            _rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            _animator = GetComponentInChildren<Animator>(); //find controller on this character, receives a normalized value

        }
        protected virtual void Update()
        {
            if (lockedByAction) return; //this is specifically how long a unit is locked out of ANY movemement or action after using an action. the "vulnerability" window
            //Debug.Log(currentActionState);
            _animator.SetFloat("Horizontal", facingDirection.x);
            _animator.SetFloat("Vertical", facingDirection.y);
            _animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing
            //Debug.Log($"the values {gameObject.name} is using to move are {_unitController.ReturnMovement().normalized}");
            movementDirection = _unitController.ReturnMovement().normalized;
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
                _rigidbody.MovePosition(_rigidbody.position + movementDirection * currentSpeed * Time.fixedDeltaTime);
            }
        }

        public void SetSpeed(float modifier)
        {
            currentSpeed = speed * modifier;
        }


        #endregion

        #region TestSection

        [SerializeField] ActionContainerScriptableObject container;
        public void TestSkillSet()
        {
            if (container == null)
            {
                Debug.Log("No Skillset inserted");
                return;
            }
            AttackContainer.LightAttack = container.lightAttack;
            AttackContainer.HeavyAttack = container.heavyAttack;
            SkillContainer.SkillOne = container.skillOne;
            SkillContainer.SkillTwo = container.skillTwo;
            SkillContainer.SkillThree = container.skillThree;
            SkillContainer.SkillFour = container.skillFour;
        }


        #endregion
    }

}