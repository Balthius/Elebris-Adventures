

using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Units
{

    [CreateAssetMenu(menuName = "Actions/Skill")]
	public class SkillScriptableObject : ActionScriptableObject, IActionBuilder
    {
		[SerializeField] private float skillDamage;
		[SerializeField] private float skillDuration;
		[SerializeField] private float skillSpeed;
	

		public float SkillDamage
		{
			get
			{
				return skillDamage;
			}
		}

		public float SkillDuration
		{
			get
			{
				return skillDuration;
			}
		}
        public float SkillSpeed
		{
			get
			{
				return skillSpeed;
			}
		}

		public IAction CreateAction()
		{
				SkillModel model = new SkillModel(
					SkillDamage, SkillDuration, SkillSpeed, actionIcon, actionName,
					animationLength, baseChargeTime, chargeMax, actionPrefab);
				IAction action = new SkillAction(model);
				return action;
		}
	}


}