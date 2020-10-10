using Assets.Scripts.Actions;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{

	[CreateAssetMenu(menuName ="Actions/Attack")]
    public class AttackScriptableObject : ActionScriptableObject, IActionBuilder
    {
		//contains all information about an attack, used by attackaction=

		[SerializeField] private int attackDamage;
		public IAction CreateAction()
		{

			AttackModel model = new AttackModel(
				attackDamage, actionIcon, actionName,
				animationLength, baseChargeTime, chargeMax, actionPrefab
				);

			IAction action = new AttackAction(model);
			return action;
		}
	}


}