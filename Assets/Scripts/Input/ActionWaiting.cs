using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class ActionWaiting : BaseChargeState
    {
        public override void Enter(Unit parent)
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
            bool actionInitiated = false;

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
                actionInitiated = _parent.UseAction(_parent.AttackContainer.LightAttack);
            }
            else if (_parent.UnitController.ChargingHeavyAttack)
            {
                //Debug.Log("ChargingHeavyAttack");
                actionInitiated = _parent.UseAction(_parent.AttackContainer.HeavyAttack);
            }

            else if (_parent.UnitController.ChargingSkillOne)
            {
                //Debug.Log("ChargingSkillOne");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillOne);
            }
            else if (_parent.UnitController.ChargingSkillTwo)
            {
                //Debug.Log("ChargingSkillTwo");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillTwo);
            }
            else if (_parent.UnitController.ChargingSkillThree)
            {
                //Debug.Log("ChargingSkillThree");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillThree);
            }
            else if (_parent.UnitController.ChargingSkillFour)
            {
                //Debug.Log("ChargingSkillFour");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillFour);
            }
            if (actionInitiated)
            {
                _parent.ChangeState(new ActionCharging());
            }
        }
    }
}
