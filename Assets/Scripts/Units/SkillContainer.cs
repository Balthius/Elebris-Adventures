using Assets.Scripts.Actions;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class SkillContainer
    {

        //change these to ActiveAction which store a ccalculated reference to each active attack.
        //They need a failsafe to check that they are updated if any of their underlying values change. 
        private ActionScriptableObject skillOne = null;
        private ActionScriptableObject skillTwo = null;
        private ActionScriptableObject skillThree = null;
        private ActionScriptableObject skillFour = null;

        //need a safety check that only SkillActions are entering (as IActions)
        private List<ActionScriptableObject> skillActions; //store all unused attacks the character has access to?

        public ActionScriptableObject SkillOne { get => skillOne; set => skillOne = value; }
        public ActionScriptableObject SkillTwo { get => skillTwo; set => skillTwo = value; }
        public ActionScriptableObject SkillThree { get => skillThree; set => skillThree = value; }
        public ActionScriptableObject SkillFour { get => skillFour; set => skillFour = value; }

        public List<ActionScriptableObject> SkillActions { get => skillActions; set => skillActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }

}