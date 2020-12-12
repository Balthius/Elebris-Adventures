using Assets.DapperEvents.GameEvents;
using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Buffs;
using Assets.Scripts.Input;
using Assets.Scripts.UI;
using Assets.Scripts.Units.UnitComponents.Shared;
using Elebris.Core.Library.Components;
using Elebris.Rpg.Library.CharacterValues;
using Elebris.Rpg.Library.StatGeneration;
using Elebris.UnitCreation.Library.StatGeneration;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Base class for all Units, Inhereted by player, character(uncontrolled hero), enemies, NPC
    /// 
    /// Control from the AI or player input is passed here, and then results are carried out by other components as needed
    /// </summary>
    public class Entity : MonoBehaviour
    {

        [Header("Target")]
        GameObject _target; 
        public Entity target
        {
            get { return _target != null ? _target.GetComponent<Entity>() : null; }
            set { _target = value != null ? value.gameObject : null; }


        }
       

        //replace with a different Status Component specifically for tracking the different statuses and their effects
        //// 3D text mesh for name above the entity's head
        //[Header("Text Meshes")]
        //public TextMeshPro stunnedOverlay;

        //// every entity can be stunned by setting stunEndTime
        //protected double stunTimeEnd;

        // safe zone flag
        // -> needs to be in Entity because both player and pet need it
        [HideInInspector] public bool inSafeZone;


        #region Core Containers and stats
        [SerializeField] protected float speed = 5f;
        protected float currentSpeed;
        protected Vector2 movementDirection = new Vector2(0, 0);
        internal Vector2 facingDirection = new Vector2(0, -1);

        public UnitData unitData;
        public UIInteractionManager uiManager;
        public BuffandStatusManager buffStatusManager;
        //public PassiveContainer PassiveContainer { get; set; }
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
            uiManager = GetComponent<UIInteractionManager>();
            unitData = GetComponent<UnitData>();
            buffStatusManager = GetComponent<BuffandStatusManager>();
            UnitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
            Rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
            Animator = GetComponentInChildren<Animator>(); //find controller on this character, receives a normalized value
            


        }
        protected virtual void Start()
        {
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


        public bool canChangeFacing = true;

       
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
        public void Recover(float val = 1)
        {
          //Trigger Recover effects on unit (as an action) with a value to pass that changes based on regular recovery, out of combat recovery etc
        }
        public virtual void OnAggro(Entity entity) { }

        public virtual bool CanAttack(Entity entity)
        {
            return (float)unitData.ValueContainer.DataHandler.RetrieveValue(ResourceStats.HealthResource) > 0 &&
                   (float)entity.unitData.ValueContainer.DataHandler.RetrieveValue(ResourceStats.HealthResource) > 0 &&
                   entity != this &&
                   !inSafeZone && !entity.inSafeZone;
                   //!NavMesh.Raycast(transform.position, entity.transform.position, out NavMeshHit hit, NavMesh.AllAreas);
        }
        protected virtual void OnDeath()
        {
            // movement/target/cast
            //ResetMovement();
            target = null;
            //CancelCastSkill();

            // clear buffs that shouldn't remain after death
            for (int i = 0; i < buffStatusManager.Buffs.Count; ++i)
            {
                if (!buffStatusManager.Buffs[i].remainAfterDeath)
                {
                    buffStatusManager.Buffs.RemoveAt(i);
                    --i;
                }
            }

        }
    //    //protected virtual void OnTriggerEnter(Collider col)
    //    //{
    //    //    // check if trigger first to avoid GetComponent tests for environment
    //    //    if (col.isTrigger && col.GetComponent<SafeZone>())
    //    //        inSafeZone = true;
    //    //}

    //    //protected virtual void OnTriggerExit(Collider col)
    //    //{
    //    //    // check if trigger first to avoid GetComponent tests for environment
    //    //    if (col.isTrigger && col.GetComponent<SafeZone>())
    //    //        inSafeZone = false;
    //    //}
    //    // for 4 years since uMMORPG release we tried to detect warps in
    //    // NetworkNavMeshAgent/Rubberbanding. it never worked 100% of the time:
    //    // -> checking if dist(pos, lastpos) > speed worked well for far teleports,
    //    //    but failed for near teleports with dist < speed meters.
    //    // -> checking if speed since last update is > speed is the perfect idea,
    //    //    but it turns out that NavMeshAgent sometimes moves faster than
    //    //    agent.speed, e.g. when moving up or down a corner/stone. in fact, it
    //    //    sometimes moves up to 5x faster than speed, which makes warp detection
    //    //    hard.
    //    // => the ONLY 100% RELIABLE solution is to have our own Warp function that
    //    //    force warps the client over the network.
    //    // => this is extremely important for cases where players get warped behind
    //    //    a small door or wall. this just has to work.
    //    // (we lose the comfort of being able to just call agent.Warp, but at least
    //    //  this method is 100% reliable)
    //    // note: abstract because players and monsters use different
    //    //       NetworkNavMeshAgent components
    //    public abstract void Warp(Vector3 destination);

    //    // same for reset movement. only way to reliably detect it is to have an
    //    // extra function. this can be called on server and on clients in case of
    //    // rubberband movement.
    //    public abstract void ResetMovement();
    }

}