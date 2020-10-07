using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBook : MonoBehaviour {
	[SerializeField]
	private WeaponAttack[] attacks;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public WeaponAttack UseAttack(int index)
	{
		return attacks [index];
	}
}
