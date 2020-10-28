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

        public Vector2 ReturnMovement()
        {
            //going to need to work on selectively locking out movement here as well for when the unit is attacking
            if (target != null)
            {
                return target.position - transform.position;
            }
            else
            {
                return Vector2.zero;
            }
        }


    }
}
