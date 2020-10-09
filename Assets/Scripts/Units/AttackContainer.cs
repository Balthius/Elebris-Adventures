using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class AttackContainer
    {
        private AttackAction lightAttack;
        private AttackAction heavyAttack;

        private List<AttackAction> attackActions; //store all unused attacks the character has access to?

        public AttackAction LightAttack { get => lightAttack; set => lightAttack = value; }
        public AttackAction HeavyAttack { get => heavyAttack; set => heavyAttack = value; }
        public List<AttackAction> AttackActions { get => attackActions; set => attackActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }
}