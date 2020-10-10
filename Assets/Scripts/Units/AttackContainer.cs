using Assets.Scripts.Actions;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class AttackContainer
    {
        private IAction lightAttack;
        private IAction heavyAttack;

        private List<IAction> attackActions; //store all unused attacks the character has access to?

        public IAction LightAttack { get => lightAttack; set => lightAttack = value; }
        public IAction HeavyAttack { get => heavyAttack; set => heavyAttack = value; }
        public List<IAction> AttackActions { get => attackActions; set => attackActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }
}