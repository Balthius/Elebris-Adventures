using UnityEngine;

namespace Assets.Scripts.Units
{
	[CreateAssetMenu(menuName ="Actions/Attack")]
    public class AttackScriptableObject : ScriptableObject
    {
		//contains all information about an attack, used by attackaction
		//this was taken from a monobehaviour, I will probably recreate it as a scriptableobject
		private string name;
		private int damage;
		private float animationLength;
		private float BaseChargeTime;

		private GameObject attackPrefab; 
        

		public string MyName
		{
			get
			{
				return name;
			}
		}

		public int MyDamage
		{
			get
			{
				return damage;
			}
		}

		public float MyAnimationLength
		{
			get
			{
				return animationLength;
			}
		}
		public GameObject AttackPrefab { get => attackPrefab; set => attackPrefab = value; }

	}


}