using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour {


	private Enemy parent;

	private void Start()
	{
		parent = GetComponentInParent <Enemy> ();

	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
            parent.SetTarget(col.transform);
            Debug.Log(col);
	
		}
        else
        {
            Debug.Log(col + "is the col");
        }
	}
	


}
