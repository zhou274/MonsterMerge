using System;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	private int GridRows = 5;

	private int GridCollumns = 5;

	public static GridSpawner instance;

	private GridTile firstTile;

	private void Awake()
	{
		GridSpawner.instance = this;
	}

	public void ClearGrid()
	{
		GridTile[] tiles = GridMap.instance.tiles;
		for (int i = 0; i < tiles.Length; i++)
		{
			GridTile gridTile = tiles[i];
			Dice[] dices = DiceCompound.instance.dices;
			for (int j = 0; j < dices.Length; j++)
			{
				Dice y = dices[j];
				if (gridTile.placedDice == y)
				{
					gridTile.tileValue = 0;
					gridTile.placedDice = null;
				}
				else
				{
					gridTile.Reset();
				}
			}
		}
		DicePointHandler.instance.ResetDiceScore();
	}

	public void SpawnGridByDuplicatingFirstTile(GridTile firstTileElement, float distance)
	{
		this.firstTile = firstTileElement;
		this.CreateRowsByFirstElement();
		this.CreateTileElementsFromRows();
		this.firstTile.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(this.firstTile.gameObject);
		GridMap.instance.CreateGridMap();
	}

	private void CreateRowsByFirstElement()
	{
		Vector3 position = this.firstTile.transform.position;
		for (int i = 0; i < this.GridRows; i++)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = position;
			GameManager.instance.gridRowsContainer.gridRows[i] = gameObject.AddComponent<GridRow>();
			position.y -= GameManager.instance.gridTileDistanceOffset;
			gameObject.name = "Row_" + i;
			gameObject.transform.parent = GameManager.instance.gridRowsContainer.transform;
		}
	}

	private void CreateTileElementsFromRows()
	{
		for (int i = 0; i < GameManager.instance.gridRowsContainer.gridRows.Length; i++)
		{
			GridRow gridRow = GameManager.instance.gridRowsContainer.gridRows[i];
			Vector3 position = gridRow.transform.position;
			for (int j = 0; j < this.GridCollumns; j++)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.firstTile.gameObject, position, this.firstTile.transform.rotation);
				GridTile component = gameObject.GetComponent<GridTile>();
				component.SetIndexes(j, i);
				gridRow.tiles[j] = component;
				position.x += GameManager.instance.gridTileDistanceOffset;
				gameObject.name = "Tile_" + j;
				gameObject.transform.parent = gridRow.transform;
			}
		}
	}
}
