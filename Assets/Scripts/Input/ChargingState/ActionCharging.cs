using Assets.Scripts.Units;
using Elebris.Core.Library.CharacterValues.Mutable;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Input
{

    public class ActionCharging : BaseChargeState
    {
        public ResourceBarValue chargeTime = new ResourceBarValue(1.5f);
        public ResourceBarValue chargeAmount = new ResourceBarValue(3);

        public override void Enter(Entity parent)
        {
            base.Enter(parent);
            _parent.canChangeFacing = false;
            _parent.Animator.SetBool("Charging", true);
            _parent.SetSpeed(_parent.unitData.ActionContainer.currentBehaviour.chargeMoveSpeed); // this should proooooobably be moved back into the unit itself as a stat modifiable

            if (_parent.unitData.ActionContainer.currentBehaviour.canCharge)
            {
                chargeTime.MaxValue = _parent.unitData.ActionContainer.currentBehaviour.baseChargeTime;
                //chargeAmount.MaxValue = (int) Get a charge max based on the type of action it is (attack, skill etc) and what level that action is
                chargeAmount.CurrentValue = -1;
                SetNextChargeTime();
            }
           
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

                _parent.ChangeState(new ActionExecuting());
            }
        }

        protected void SetNextChargeTime()
        {
            chargeAmount.CurrentValue++;

            chargeTime.MaxValue *= .8f; //quicker charges as it gets higher //place in constants
            _parent.StartCoroutine(CheckChargeAction(chargeTime.MaxValue));
            //Debug.Log($"{chargeTime.MaxValue} max time, {chargeTime.CurrentValue} current time  and then abse charge time{_actionPrototype.baseChargeTime}");

        }

        public IEnumerator CheckChargeAction(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (!ChargeMaxed())
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
