using Assets.Scripts.Actions.Attacks;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class AttackContainer
    {
        //change these to ActiveAction which store a ccalculated reference to each active attack.
        //They need a failsafe to check that they are updated if any of their underlying values change. 
        private StoredAction lightAttack;
        private StoredAction heavyAttack;


        public StoredAction LightAttack
        {
            get => lightAttack; set
            {

                lightAttack = value;
            }
        }
        public StoredAction HeavyAttack { get => heavyAttack; set => heavyAttack = value; }
        private List<ActionScriptableObject> attackActions; //store all unused attacks the character has access to?
        public List<ActionScriptableObject> AttackActions { get => attackActions; set => attackActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }
}