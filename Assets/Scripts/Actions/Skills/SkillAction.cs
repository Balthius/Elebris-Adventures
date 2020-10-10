

using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Units
{


    public class SkillAction : BaseAction, IAction
    {

        public SkillAction(SkillModel model)
        {
            this.actionName = model.actionName;
            this.actionIcon = model.actionIcon;
            this.actionDamage = model.skillDamage;
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

            //check characters specific skill variations and adjust numbers accordingly, then return an action for them to use
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