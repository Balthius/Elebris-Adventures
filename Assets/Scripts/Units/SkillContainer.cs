using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class SkillContainer
    {

        private SkillScriptableObject skillOne = null;
        private SkillScriptableObject skillTwo = null;
        private SkillScriptableObject skillThree = null;
        private SkillScriptableObject skillFour = null;

        private List<SkillScriptableObject> skillActions; //store all unused attacks the character has access to?

        public SkillScriptableObject SkillOne { get => skillOne; set => skillOne = value; }
        public SkillScriptableObject SkillTwo { get => skillTwo; set => skillTwo = value; }
        public SkillScriptableObject SkillThree { get => skillThree; set => skillThree = value; }
        public SkillScriptableObject SkillFour { get => skillFour; set => skillFour = value; }

        public List<SkillScriptableObject> SkillActions { get => skillActions; set => skillActions = value; }
        //need to work out how to string together combos too if I decide to take that route (combination of light and heavy attacks in sequence)
    }

}