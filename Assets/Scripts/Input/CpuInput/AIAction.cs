
using System.Collections.Generic;
namespace Assets.Scripts.Units
{
    //This should be moved to the DLL
    public partial class AIAction
    {
        public float chargeDuration = 1;
        public float actionWeightIncrease = 50; //default parameter for how much to increase Weight by after use
        public float actionRange = 1;

        public float currentWeight = 100;
        public ActionSlot slot = ActionSlot.select;

        public void ChangeWeight(float amount = -5)
        {
            currentWeight += amount;
        }

        public void SubscribeItem(AIActionContainer actionList, ActionSlot slot)
        {
            actionList.SubscribeAction(this);
            this.slot = slot;
        }

        public void ActivateAction()
        {
            ChangeWeight(actionWeightIncrease);
        }

        public bool CheckAction(float distance)
        {
            if(actionRange > distance)
            {
                ChangeWeight(actionWeightIncrease);
                return true;
            }
            return false;
        }
    }
}
