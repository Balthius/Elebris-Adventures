using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable]
public class Skill : IUseable
{
	[SerializeField]
	private string name;
	[SerializeField]
	private int damage;
	[SerializeField]
	private float duration;
	[SerializeField]
	private Sprite icon;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float animationLength;
	[SerializeField]
	private GameObject spellPrefab;

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

	public GameObject MySpellPrefab
	{
		get 
		{
			return spellPrefab;
		}
	}

    public void Use()
    {
        Player.MyInstance.SkillAttack(MyName);
    }
}
