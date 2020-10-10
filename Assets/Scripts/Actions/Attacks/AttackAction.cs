using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class AttackAction : BaseAction, IAction
    {
        //2 lists to add down the road will implement combatcalculations to ignore, and combatcalculations to add. things like lifesteal or really...anything
        //ignored calculations are specialty cases where things like shield etc are stripped from the list of calculations to perform
        public AttackAction(AttackModel model)
        {
            //should I just store the model for use? or can they perform necessary calculations in the constructor and then be used from there?
            this.name = model.actionName;
            this.actionIcon = model.actionIcon;
            this.actionDamage = model.attackDamage;
            this.animationLength = model.animationLength;
            this.baseChargeTime = model.baseChargeTime;
            this.chargeMax = model.chargeMax;
            this.actionPrefab = model.actionPrefab;
        }

        public int ChargeMax()
        {
            return chargeMax;
        }

        public void Implement(Unit unit)
        {
            //check characters specific attack variations and adjust numbers accordingly, then return an action for them to use
        }

        public GameObject CreateObject()
        {
            return actionPrefab;
        }

        public float ChargeTime()
        {
            return baseChargeTime;

        }
    }


}