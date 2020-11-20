using Assets.Scripts.Actions.Attacks;
using Elebris.Actions.Library.Actions.Calculations;
using Elebris.Actions.Library.Actions.Component;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Core.Library.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class MonoPassiveComponent : PassiveComponent
    {

        public List<ComponentValueModel> ActionValues { get => actionObjects; set => actionObjects = value; }
        public Dictionary<string, ActionCalculation> ActionCalculations { get => actionCalculations; set => actionCalculations = value; }

        private List<ComponentValueModel> actionObjects = new List<ComponentValueModel>();

        private Dictionary<string, ActionCalculation> actionCalculations = new Dictionary<string, ActionCalculation>();
        public void ExecuteOnAction(CharacterAction action, MonoUnit character)
        {
            //based on tags in each componentValueModel you will affect actions both created and received
            foreach (var actionObj in ActionValues)
            {
                float scaleValue = actionObj.flat + (character.Character.CharacterResources.RetrieveCharacterValue(actionObj.targetStat) * actionObj.scale);
                action.InsertValue(actionObj.name, new ValueModifier(scaleValue, ValueModEnum.Flat));
                action.InsertValue(actionObj.name, new ValueModifier(actionObj.percent, ValueModEnum.PercentAdd));
                action.InsertValue(actionObj.name, new ValueModifier(actionObj.multiplier, ValueModEnum.PercentMult));
            }

            foreach (var calculation in actionCalculations.Values)
            {
                string name = calculation.GetType().ToString();
                action.InsertCalculations(name, calculation);
            }
        }
        public void ExecuteOnCharacter(MonoUnit unit)
        {
            //run once at start, these are things that purely boost units with no trigger
        }
    }
}