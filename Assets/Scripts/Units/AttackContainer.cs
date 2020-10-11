using Assets.Scripts.Actions;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class AttackContainer
    {
        private ActionScriptableObject lightAttack;
        private ActionScriptableObject heavyAttack;

        private List<ActionScriptableObject> attackActions; //store all unused attacks the character has access to?

        public ActionScriptableObject LightAttack
        {
            get => lightAttack; set
            {
               
                lightAttack = value;
            }
        }
        public ActionScriptableObject HeavyAttack { get => heavyAttack; set => heavyAttack = value; }
        public List<ActionScriptableObject> AttackActions { get => attackActions; set => attackActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }
}