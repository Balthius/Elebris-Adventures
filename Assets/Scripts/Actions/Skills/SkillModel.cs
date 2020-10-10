using UnityEngine;

namespace Assets.Scripts.Units
{
    public struct SkillModel
    {

		public float skillDamage;
		public float skillDuration;
		public float skillSpeed;
		public Sprite actionIcon;
		public string actionName;
		public float animationLength;
		public float baseChargeTime;
		public int chargeMax;
		public GameObject actionPrefab;

        public SkillModel(float skillDamage, float skillDuration, float skillSpeed, Sprite actionIcon, string actionName, float animationLength, float baseChargeTime, int chargeMax, GameObject actionPrefab)
        {
            this.skillDamage = skillDamage;
            this.skillDuration = skillDuration;
            this.skillSpeed = skillSpeed;
            this.actionIcon = actionIcon;
            this.actionName = actionName;
            this.animationLength = animationLength;
            this.baseChargeTime = baseChargeTime;
            this.chargeMax = chargeMax;
            this.actionPrefab = actionPrefab;
        }
    }


}