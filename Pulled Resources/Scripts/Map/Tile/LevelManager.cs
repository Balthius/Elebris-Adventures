using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private Texture2D[] mapData;

	[SerializeField]
	private MapElement[] mapElements;

	[SerializeField]
	private Sprite defaultSprite;


	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	[Serializable]
	public class MapElement
	{
		[SerializeField]
		private string tileTag;


		[SerializeField]
		private Color color;

		[SerializeField]
		private GameObject elementPrefab;

		public GameObject MyElementPrefab
		{
			get
			{
				return elementPrefab;
			}
		}
		public Color MyColor
		{
			get
			{
				return color;
			}
		}

		public string MyTileTag
		{
			get
			{
				return tileTag;
			}
		}
	}
}
