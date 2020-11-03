using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool actionInitiated = false; ;

            if (_unitController.ChargingSelect)
            {
                //UseAction();
                //Debug.Log("ChargingSelect");
            }
            else if (_unitController.ChargingManeuver)
            {
                //Debug.Log("ChargingManeuver");
            }

            else if (_unitController.ChargingLightAttack)
            {
                //Debug.Log("ChargingLightAttack");
                actionInitiated = _parent.UseAction(_parent.AttackContainer.LightAttack);
            }
            else if (_unitController.ChargingHeavyAttack)
            {
                //Debug.Log("ChargingHeavyAttack");
                actionInitiated = _parent.UseAction(_parent.AttackContainer.HeavyAttack);
            }

            else if (_unitController.ChargingSkillOne)
            {
                //Debug.Log("ChargingSkillOne");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillOne);
            }
            else if (_unitController.ChargingSkillTwo)
            {
                //Debug.Log("ChargingSkillTwo");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillTwo);
            }
            else if (_unitController.ChargingSkillThree)
            {
                //Debug.Log("ChargingSkillThree");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillThree);
            }
            else if (_unitController.ChargingSkillFour)
            {
                //Debug.Log("ChargingSkillFour");
                actionInitiated = _parent.UseAction(_parent.SkillContainer.SkillFour);
            }
            if(actionInitiated)
            {
                _parent.ChangeState(new ActionCharging());
            }
        }
    }
}
