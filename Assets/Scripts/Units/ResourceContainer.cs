using Elebris.Library.Units;
using Elebris.Library.Values;

namespace Assets.Scripts.Units
{
    public class ResourceContainer
    {
        private ValueHolder healthValue = new ValueHolder(100, 0, StatsEnum.HealthResource);

        public ValueHolder HealthValue { get => healthValue; set => healthValue = value; }


        private ValueHolder spiritValue = new ValueHolder(10, 0, StatsEnum.SpiritResource);

        public ValueHolder SpiritValue { get => spiritValue; set => spiritValue = value; }


        private ValueHolder staminaValue = new ValueHolder(10, 0, StatsEnum.StaminaResource);

        public ValueHolder StaminaValue { get => staminaValue; set => staminaValue = value; }


        private ValueHolder manaValue = new ValueHolder(10, 0, StatsEnum.ManaResource);

        public ValueHolder ManaValue { get => manaValue; set => manaValue = value; }


    }
}