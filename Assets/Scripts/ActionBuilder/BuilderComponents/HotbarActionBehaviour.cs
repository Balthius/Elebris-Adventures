using Elebris.Rpg.Library.Actions.Core;
using System;

namespace Assets.Scripts.ActionBuilder.BuilderComponents
{
    [Serializable]
    public struct HotbarActionBehaviour
    {
        public float baseChargeTime; // time to increase the skills charge++
        public float actionLock; //Time user is unable to act after using the action

        public float actionCooldown; // time before you can reuse the action
        public bool canCharge; //if the action can charge at all
        public float chargeMoveSpeed;
        public ResourceCost[] ResourceCosts;
        public HotbarActionBehaviour(float baseChargeTime, float actionLock, float actionCooldown,
            bool canCharge, float chargeMoveSpeed, params ResourceCost[] costs)
        {
            this.baseChargeTime = baseChargeTime;
            this.actionLock = actionLock;
            this.actionCooldown = actionCooldown;
            this.canCharge = canCharge;
            this.chargeMoveSpeed = chargeMoveSpeed;
            ResourceCosts = costs;
        }
    }
}