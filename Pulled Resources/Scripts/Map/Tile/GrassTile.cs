using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GrassTile : Tile
{
	[SerializeField]
	private Sprite[] grassSprites;


	[SerializeField]
	private Sprite preview;


	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		for (int y = -1; y <= 1 ; y++ )
		{ 
			for (int x = -1; x <= 1; x++)
			{
				Vector3Int nPos = new Vector3Int (position.x + x, position.y + y, position.z);

				if (HasGrass(tilemap,nPos))
				{
					tilemap.RefreshTile (nPos);
				}
			}
		}
	}

	public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
	{
		string composition = string.Empty;

		for (int x = -1; x <= 1; x++)//Runs through all neighbours
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x != 0 || y != 0) //Makes sure that we aren't checking our self
				{
					//If the value is a grasstile
					if (HasGrass(tilemap, new Vector3Int(location.x + x, location.y + y, location.z)))
					{
						composition += 'W';
					}
					else
					{
						composition += 'E';
					}


				}
			}
		}

		int randomVal = Random.Range(0, 200);

		if (randomVal < 5)
		{
			tileData.sprite = grassSprites[0];
		}
		else if (randomVal >= 5 && randomVal < 10)
		{

			tileData.sprite = grassSprites[1];
		}
		else if (randomVal >= 11 && randomVal < 15)
		{

			tileData.sprite = grassSprites[2];
		}
		else if (randomVal >= 16 && randomVal < 20)
		{

			tileData.sprite = grassSprites[3];
		}
		else if (randomVal >= 21 && randomVal < 25)
		{

			tileData.sprite = grassSprites[4];
		}
		else if (randomVal >= 26 && randomVal < 30)
		{

			tileData.sprite = grassSprites[5];
		}
		else if (randomVal >= 31 && randomVal < 35)
		{

			tileData.sprite = grassSprites[6];
		}
		else if (randomVal >= 36 && randomVal < 100)
		{

			tileData.sprite = grassSprites[7];
		}
		else
		{
			tileData.sprite = grassSprites[8];
		}



	}

	private bool HasGrass(ITilemap tilemap, Vector3Int position)
	{
		return tilemap.GetTile (position) == this;
	}

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/GrassTile")]
	public static void CreateGrassTile()
	{
		string path = EditorUtility.SaveFilePanelInProject ("Save GrassTile", " New Grasstile", "asset", "Save grasstile", "Assets");
		if (path == "")
		{
			return;
		}
		AssetDatabase.CreateAsset (ScriptableObject.CreateInstance <GrassTile>(), path);
	}

	#endif
}
