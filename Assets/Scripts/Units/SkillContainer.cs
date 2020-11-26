using Assets.Scripts.Actions.Attacks;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class SkillContainer
    {

        //change these to ActiveAction which store a ccalculated reference to each active attack. as well as a monitored cooldown valueholder
        //They need a failsafe to check that they are updated if any of their underlying values change. 
        private BoundAction skillOne = null;
        private BoundAction skillTwo = null;
        private BoundAction skillThree = null;
        private BoundAction skillFour = null;

        //need a safety check that only SkillActions are entering (as IActions)

        public BoundAction SkillOne { get => skillOne; set => skillOne = value; }
        public BoundAction SkillTwo { get => skillTwo; set => skillTwo = value; }
        public BoundAction SkillThree { get => skillThree; set => skillThree = value; }
        public BoundAction SkillFour { get => skillFour; set => skillFour = value; }

        private List<ActionScriptableObject> skillActions; //store all unused attacks the character has access to?
        public List<ActionScriptableObject> SkillActions { get => skillActions; set => skillActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }

}