using Assets.Scripts.Actions;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class SkillContainer
    {

        private IAction skillOne = null;
        private IAction skillTwo = null;
        private IAction skillThree = null;
        private IAction skillFour = null;

        //need a safety check that only SkillActions are entering (as IActions)
        private List<IAction> skillActions; //store all unused attacks the character has access to?

        public IAction SkillOne { get => skillOne; set => skillOne = value; }
        public IAction SkillTwo { get => skillTwo; set => skillTwo = value; }
        public IAction SkillThree { get => skillThree; set => skillThree = value; }
        public IAction SkillFour { get => skillFour; set => skillFour = value; }

        public List<IAction> SkillActions { get => skillActions; set => skillActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }

}