using System.Collections.Generic;
using Assets.Scripts.Actions.Attacks;
using Elebris.Actions.Library.Actions.Core;

namespace Assets.Scripts.Units
{
    public class PassiveContainer
    {
        public List<MonoPassiveComponent> passives = new List<MonoPassiveComponent>();
       
        public void ModifyAction(ActionBase baseAction, MonoUnit unit)
        {
            //execute on every action that passes through
            foreach (var passive in passives)
            {
                passive.ExecuteOnAction(baseAction.ActionPacket, unit);
            }
        }

        public void ModifyCharacter(MonoUnit unit)
        {
            //execute selectively
            foreach (var passive in passives)
            {
                passive.ExecuteOnCharacter(unit);
            }
        }

        //separate method that takes a list of all nearby units and applies relevant passives? (auras, DoT etc)

    }
}