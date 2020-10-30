using UnityEngine;


/// <summary>
/// This class is responsible for passing Input from the player to the Active character class. IUnitController is the only public portion of 
/// this class so you should only call this via the interface
/// Other classes to control AI input need to implement the IUnitcontroller separately
/// </summary>

namespace Assets.Scripts.Units
{
    public class CpuInputController : MonoBehaviour, IUnitController, IAIController
    {
        //implement a FSM that feeds info into the controller?
        //"initialize" the weights of each skill, percentages units perform certain actions at, whatever
        //target and aggro range/type.
        //all of the above sound like good contained classes
        public bool ChargingLightAttack { get; set ; }
        public bool ChargingHeavyAttack { get; set ; }

        public bool ChargingSkillOne { get; set ; }

        public bool ChargingSkillTwo { get; set ; }

        public bool ChargingSkillThree { get; set ; }

        public bool ChargingSkillFour { get; set; }

        public bool ChargingManeuver { get; set; }
        public bool ChargingSelect { get; set; }
        public Transform Target { get => target; set => target = value; }

        private Transform target;
        private IState currentState;

        public float AttackRange { get; set; }
        public Vector2 CurrentMovement { get; set; }
        public Vector2 ReturnMovement()
        {
            return CurrentMovement;
        }

        private void Awake()
        {
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

        public void Initialize(float attackRange)
        {
            AttackRange = attackRange;
        }
    }
}
