using Elebris.Library.Units;
using Elebris.Library.Values;

namespace Assets.Scripts.Units
{
    public class ResourceContainer
    {
        private ValueHolder healthValue = new ValueHolder(100,0, StatsEnum.MaximumHealth);

        public ValueHolder HealthValue { get => healthValue; set => healthValue = value; }


    }
}