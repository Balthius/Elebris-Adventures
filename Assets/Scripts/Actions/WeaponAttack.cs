using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
	public class WeaponAttack
{
	[SerializeField]
	private string name;
	[SerializeField]
	private int damage;
	[SerializeField]
	private float animationLength;
	[SerializeField]
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

	public GameObject MyAttackPrefab
	{
		get 
		{
			return attackPrefab;
		}
	}


}
