

using System;

namespace Elebris.Library.Units
{
    public class Character{

         int startingLevel = 1;
        public int currentLevel;
        public event Action onLevelUp;  
        
        private CharacterResourceSystem characterResources= null;
        private CharacterGear characterGear = null;
        private Experience characterExperience = null;

        public CharacterResourceSystem CharacterResources { get => characterResources; set => characterResources = value; }
        public CharacterGear CharacterGear { get => characterGear; set => characterGear = value; }
        public Experience CharacterExperience { get => characterExperience; set => characterExperience = value; }
    }
}