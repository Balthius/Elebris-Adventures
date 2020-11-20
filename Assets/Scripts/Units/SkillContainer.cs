using Assets.Scripts.Actions.Attacks;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class SkillContainer
    {

        //change these to ActiveAction which store a ccalculated reference to each active attack. as well as a monitored cooldown valueholder
        //They need a failsafe to check that they are updated if any of their underlying values change. 
        private StoredAction skillOne = null;
        private StoredAction skillTwo = null;
        private StoredAction skillThree = null;
        private StoredAction skillFour = null;

        //need a safety check that only SkillActions are entering (as IActions)

        public StoredAction SkillOne { get => skillOne; set => skillOne = value; }
        public StoredAction SkillTwo { get => skillTwo; set => skillTwo = value; }
        public StoredAction SkillThree { get => skillThree; set => skillThree = value; }
        public StoredAction SkillFour { get => skillFour; set => skillFour = value; }

        private List<ActionScriptableObject> skillActions; //store all unused attacks the character has access to?
        public List<ActionScriptableObject> SkillActions { get => skillActions; set => skillActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }

}