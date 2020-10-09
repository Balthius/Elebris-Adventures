using Elebris.Library.Values;
using RPG.CharacterValues;

namespace Elebris.Library.Units
{
    //every value that functions on a full/empty system should use this, especially health and resources
    public class ValueHolder
    {
        public float currentValue = 10;
        public float maxValue = 10;
        public float missingValue = 0;
        public StatsEnum type;

        public ValueHolder(float maxValue, float missingValue, StatsEnum type)
        {
            this.maxValue = maxValue;
            this.missingValue = missingValue;
            this.type = type;
            currentValue = maxValue - missingValue;//Calculated this way to grant some resources on levelup but not go through a full-reset method
        }
    }
    
}