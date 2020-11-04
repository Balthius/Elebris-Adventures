using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using Elebris.Library.Enums;
using Elebris.Library.Units;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Input
{

    public class ActionCharging : BaseChargeState
    {
        public ValueHolder chargeTime = new ValueHolder(1.5f, 0, StatsEnum.HealthResource);
        public ValueHolder chargeAmount = new ValueHolder(3, 0, StatsEnum.HealthResource);// placeholder Enum. Needs to either be a nullable value or removed from the valueholder and put in a child class

        public override void Enter(Unit parent)
        {
            base.Enter(parent);
            _parent.canChangeFacing = false;
            _action = _parent.CurrentAction;
            _parent.Animator.SetBool("Charging", true);
            _parent.SetSpeed(.75f); // this should proooooobably be moved back into the unit itself
            SetNextChargeTime();

            chargeTime.MaxValue = _parent.ActionPrototype.baseChargeTime;
            chargeAmount.MaxValue = _parent.ActionPrototype.chargeMax;
            chargeAmount.CurrentValue = -1;
        }

        public override void Exit()
        {
            base.Exit();

            _parent.Animator.SetBool("Charging", false);

            _parent.canChangeFacing = true;
            _parent.SetSpeed(1);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            ChargeAction();
            //only reachable once a skill has been prepared to use, so other inputs should be disabled and this should only fire off at a "safe time"
            //it iS ugly, so I'll need to find a better way to loop the values. I should feel guilty about this but I also have so little experience with the new input system.
            //Debug.Log("Checking release");
            if (!_parent.UnitController.ChargingSelect &&
                !_parent.UnitController.ChargingLightAttack &&
                !_parent.UnitController.ChargingHeavyAttack &&
                !_parent.UnitController.ChargingManeuver &&
                !_parent.UnitController.ChargingSkillOne &&
                !_parent.UnitController.ChargingSkillTwo &&
                !_parent.UnitController.ChargingSkillThree &&
                !_parent.UnitController.ChargingSkillFour
                )
            {
                _parent.SpawnAction((int)chargeAmount.CurrentValue);
                _parent.ChangeState(new ActionExecuting());
            }

        }

        protected void SetNextChargeTime()
        {
            chargeAmount.CurrentValue++;

           chargeTime.MaxValue *= .8f; //quicker charges as it gets higher
            _parent.StartCoroutine(CheckChargeAction(chargeTime.MaxValue));
            //Debug.Log($"{chargeTime.MaxValue} max time, {chargeTime.CurrentValue} current time  and then abse charge time{_actionPrototype.baseChargeTime}");

        }

        public IEnumerator CheckChargeAction(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (ChargeMaxed())
            {
                SetNextChargeTime();
            }
        }
        public bool ChargeMaxed()
        {
            if (chargeAmount.CurrentValue < chargeAmount.MaxValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ChargeAction()
        {
            if (chargeAmount.MaxValue == chargeAmount.CurrentValue)
            {
                //visual and audio effect for max charge
            }
        }
    }
}
