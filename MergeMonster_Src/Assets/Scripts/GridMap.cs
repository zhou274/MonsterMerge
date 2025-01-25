using System;
using UnityEngine;

public class GridMap : MonoBehaviour
{
	public GridTile[] tiles;

	public static GridMap instance;

	private void Awake()
	{
		GridMap.instance = this;
	}

	public void CreateGridMap()
	{
		this.tiles = UnityEngine.Object.FindObjectsOfType<GridTile>();
	}
}
