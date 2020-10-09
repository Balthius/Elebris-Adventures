

using UnityEngine;

namespace Assets.Scripts.Units
{

	[CreateAssetMenu(menuName = "Actions/Attack")]
	public class SkillScriptableObject : ScriptableObject
    {
		//this was taken from a monobehaviour, I will probably recreate it as a scriptableobject
		private string name;
		private int damage;
		private float duration;
		private Sprite icon;
		[SerializeField]
		private float speed;
		private float animationLength;
	

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

		public float MyDuration
		{
			get
			{
				return duration;
			}
		}

        public Sprite MyIcon
        {
            get
            {
                return icon;
            }
        }

        public float MySpeed
		{
			get
			{
				return speed;
			}
		}


		public float MyAnimationLength
		{
			get
			{
				return animationLength;
			}
		}
       

    }

}