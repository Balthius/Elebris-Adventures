using UnityEngine;

namespace Assets.Scripts.Units
{
    public struct AttackModel
    {
        public float attackDamage;
        public Sprite actionIcon;
        public string actionName;
        public float animationLength;
        public float baseChargeTime;
        public int chargeMax;
        public GameObject actionPrefab;

        public AttackModel(float attackDamage, Sprite actionIcon, string actionName, float animationLength, float baseChargeTime, int chargeMax, GameObject actionPrefab)
        {
            this.attackDamage = attackDamage;
            this.actionIcon = actionIcon;
            this.actionName = actionName;
            this.animationLength = animationLength;
            this.baseChargeTime = baseChargeTime;
            this.chargeMax = chargeMax;
            this.actionPrefab = actionPrefab;
        }
    }


}