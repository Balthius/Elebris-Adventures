using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// This class is responsible for passing Input from the player to the Active character class. IUnitController is the only public portion of 
/// this class so you should only call this via the interface
/// Other classes to control AI input need to implement the IUnitcontroller separately
/// </summary>

namespace Assets.Scripts.Units
{
    public class CpuInputController : MonoBehaviour, IUnitController
    {
        //implement a FSM that feeds info into the controller?
        //"initialize" the weights of each skill, percentages units perform certain actions at, whatever
        //target and aggro range/type.
        //all of the above sound like good contained classes
        public bool ChargingLightAttack { get; set; }
        public bool ChargingHeavyAttack { get; set; }

        public bool ChargingSkillOne { get; set; }

        public bool ChargingSkillTwo { get; set; }

        public bool ChargingSkillThree { get; set; }

        public bool ChargingSkillFour { get; set; }

        public bool ChargingManeuver { get; set; }
        public bool ChargingSelect { get; set; }
        public Transform Target { get => target; set => target = value; }

        private Transform target;
        private IState currentState;
        public AIActionContainer ActionContainer { get; set; } = new AIActionContainer(); //contains a list (scriptable object?) of each skill available to the unit

        public AIAction currentAction;
        public Vector2 CurrentMovement { get; set; }
        public Vector2 ReturnMovement()
        {
            return CurrentMovement;
        }

        public float AttemptAttackRange { get; set; } = 2;//distance at which at least one action will be within range

        public float AttemptRetreatRange { get; set; } = .3f;//distance at which at least one action will be within range

        public float DistanceFromTarget {
            get
            {
                if (target != null)
                {
                    return Vector2.Distance(Target.position, transform.position);
                }
                else
                {
                    return 9999;
                }
            }
        }

        public bool UsingAction { get; set; }
        public bool UnitPanicked { get; set; }

        private void Awake()
        {
            ActionContainer.GroupActions();
            SetAttackThreshold();
            ChangeState(new IdleState());
        }

        private void Update()
        {
            currentState.UpdateState();
        }

        public void ChangeState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = newState;
            currentState.Enter(this);
        }

        public void EndAction()
        {
            UsingAction = false;
            ChargingLightAttack = false;
            ChargingHeavyAttack = false;
            ChargingSkillOne = false;
            ChargingSkillTwo = false;
            ChargingSkillThree = false;
            ChargingSkillFour = false;
            ChargingManeuver = false;
            //sets next action to use
        }


        private void SetAttackThreshold()
        {
            //Debug.Log("Reached");
            foreach (AIAction item in ActionContainer.actionList)
            {
                if(item.maxRange > AttemptAttackRange)
                {
                    AttemptAttackRange = item.maxRange;
                    //Debug.Log("Enemy attack range" + AttemptAttackRange);
                }
                if( item.minRange < AttemptRetreatRange)
                {
                    AttemptRetreatRange = item.minRange;
                }
            }
        }
    }

}
