using Assets.Scripts.ActionBuilder.BuilderComponents;
using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Rpg.Library.CharacterValues;
/// <summary>
/// https://gamedev.stackexchange.com/questions/46819/making-characters-skills-and-abilities-as-commands-good-practice
/// </summary>

public interface IActionBuilder
{
    BaseActionData ReturnActionData();
    void Modifybehaviour(ref HotbarActionBehaviour behaviour, CharacterValueContainer container);
    void ModifyAction(ref PreparedAction action, CharacterValueContainer container);

}
