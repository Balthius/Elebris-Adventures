using Elebris.Core.Library.CharacterValues.Mutable;

namespace Assets.Scripts.Actions.Attacks
{
    public class BoundAction : ActionBase
    {
        ValueHolder Cooldown { get; set; } //retrieve cooldown from the cooldown stat, default value(when cd not found can be .1 seconds?
    }
}