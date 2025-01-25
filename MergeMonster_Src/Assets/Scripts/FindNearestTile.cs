using System;
using UnityEngine;

public static class FindNearestTile
{
	public static DistancedTile find(Vector3 dropPosition)
	{
		GridTile t = null;
		float num = float.PositiveInfinity;
		for (int i = 0; i < GridMap.instance.tiles.Length; i++)
		{
			GridTile gridTile = GridMap.instance.tiles[i];
			float num2 = Vector3.Distance(gridTile.transform.position, dropPosition);
			if (num2 < num)
			{
				num = num2;
				t = gridTile;
			}
		}
		DistancedTile distancedTile = new DistancedTile();
		distancedTile.Create(t, num);
		return distancedTile;
	}
}
