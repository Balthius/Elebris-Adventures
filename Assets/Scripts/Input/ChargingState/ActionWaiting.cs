using Assets.Scripts.Units;
using Elebris.Core.Library.Components;

namespace Assets.Scripts.Input
{
    public class ActionWaiting : BaseChargeState
    {
        public override void Enter(Entity parent)
        {
            base.Enter(parent);
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (boundAction != null)
            {
                return;
            }

            if (_parent.UnitController.ChargingSelect)
            {
                //UseAction();
                //Debug.Log("ChargingSelect");
            }
            else if (_parent.UnitController.ChargingManeuver)
            {
                //Debug.Log("ChargingManeuver");
            }

            else if (_parent.UnitController.ChargingLightAttack)
            {
                //Debug.Log("ChargingLightAttack");
                UseAction(BindableActions.LightAttack);
            }
            else if (_parent.UnitController.ChargingHeavyAttack)
            {
                //Debug.Log("ChargingHeavyAttack");
                UseAction(BindableActions.HeavyAttack);
            }

            else if (_parent.UnitController.ChargingSkillOne)
            {
                //Debug.Log("ChargingSkillOne");
                UseAction(BindableActions.SkillOne);
            }
            else if (_parent.UnitController.ChargingSkillTwo)
            {
                //Debug.Log("ChargingSkillTwo");
                UseAction(BindableActions.SkillTwo);
            }
            else if (_parent.UnitController.ChargingSkillThree)
            {
                //Debug.Log("ChargingSkillThree");
                UseAction(BindableActions.SkillThree);
            }
            else if (_parent.UnitController.ChargingSkillFour)
            {
                //Debug.Log("ChargingSkillFour");
                UseAction(BindableActions.SkillFour);
            }
            if (boundAction != null)
            {
                _parent.ChangeState(new ActionCharging());
            }
        }

        private void UseAction(BindableActions actions)
        {
            HotbarBoundAction bound = _parent.unitData.ActionContainer.CharacterActions[actions];
           
            if (boundAction.CanCast(_parent.unitData.ValueContainer))
            {
                boundAction = bound;
            }
            
               
        }
    }
}
