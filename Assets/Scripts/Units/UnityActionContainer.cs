using Elebris.Rpg.Library.Actions.Core;
using System.Collections.Generic;

namespace Assets.Scripts.Units
{
    public class UnityActionContainer
    {
        public UnityBoundAction LightAttack { get; set; } = new UnityBoundAction(ActionType.Attack);
        public UnityBoundAction HeavyAttack { get; set; } = new UnityBoundAction(ActionType.Attack);
        public UnityBoundAction Maneuver { get; set; } = new UnityBoundAction(ActionType.Maneuver);
        public UnityBoundAction SkillOne { get; set; } = new UnityBoundAction(ActionType.Skill);
        public UnityBoundAction SkillTwo { get; set; } = new UnityBoundAction(ActionType.Skill);
        public UnityBoundAction SkillThree { get; set; } = new UnityBoundAction(ActionType.Skill);
        public UnityBoundAction SkillFour { get; set; } = new UnityBoundAction(ActionType.Skill);

        private List<ActionReference> skillActions; //store all unused skills the character has access to?
        private List<ActionReference> SkillActions { get => skillActions; set => skillActions = value; }
        private List<ActionReference> LinkedSkills { get; set; } //cannot be equipped

        private List<ActionReference> attackActions; //store all unused attacks the character has access to?
        private List<ActionReference> AttackActions { get => skillActions; set => skillActions = value; }
        private List<ActionReference> LinkedAttacks { get; set; } //cannot be equipped

        private List<ActionReference> maneuverActions; //store all unused attacks the character has access to?
        private List<ActionReference> ManeuverActions { get => skillActions; set => skillActions = value; }
        private List<ActionReference> LinkedManeuvers { get; set; } //cannot be equipped

        //when you select a skill for a slot, it will populate into the bound action
        public void EquipAction(int position, ActionType type, BoundAction action)
        {
            switch (type)
            {
                case ActionType.Attack:
                    RunEquip(LinkedManeuvers, maneuverActions, position, action);
                    break;
                case ActionType.Skill:
                    RunEquip(LinkedManeuvers, maneuverActions, position, action);
                    break;
                case ActionType.Maneuver:
                    RunEquip(LinkedManeuvers, maneuverActions, position, action);
                    break;
                case ActionType.Social:

                    //not implemented
                    break;
                default:
                    break;
            }

        }
        public void RunEquip(List<ActionReference> link, List<ActionReference> stored, int position, BoundAction action)
        {
            ActionReference temp = stored[position];
            action.AddAction(temp);
            stored.RemoveAt(position);
            link.Add(temp);
        }
        public void UnequipAction(int position, ActionType type)
        {
            switch (type)
            {
                case ActionType.Attack:
                    RunUnequip(LinkedAttacks, attackActions, position);
                    break;
                case ActionType.Skill:
                    ActionReference stemp = LinkedSkills[position];

                    RunUnequip(LinkedSkills, skillActions, position);
                    break;
                case ActionType.Maneuver:

                    RunUnequip(LinkedManeuvers, maneuverActions, position);
                    break;
                case ActionType.Social:

                    //not implemented
                    break;
                default:
                    break;
            }

        }
        private void RunUnequip(List<ActionReference> link, List<ActionReference> stored, int position)
        {
            ActionReference temp = link[position];
            temp.Unsubscribe();
            link.RemoveAt(position);
            stored.Add(temp);
        }

        public void AddAction(IActionBehaviour behaviour, ActionType type)
        {
            ActionReference actRef = new ActionReference(behaviour);
            switch (type)
            {
                case ActionType.Attack:
                    attackActions.Add(actRef);
                    break;
                case ActionType.Skill:
                    skillActions.Add(actRef);
                    break;
                case ActionType.Maneuver:
                    maneuverActions.Add(actRef);
                    break;
                case ActionType.Social:
                    //not implemented
                    break;
                default:
                    break;
            }
        }
        public void RemoveAction(ActionReference behaviour, ActionType type)
        {

            behaviour.Unsubscribe();
            switch (type)
            {
                case ActionType.Attack:
                    attackActions.Remove(behaviour);
                    break;
                case ActionType.Skill:
                    skillActions.Remove(behaviour);
                    break;
                case ActionType.Maneuver:
                    maneuverActions.Remove(behaviour);
                    break;
                case ActionType.Social:
                    //not implemented
                    break;
                default:
                    break;
            }
        }

    }
}