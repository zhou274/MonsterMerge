using System;
using System.Collections.Generic;

public static class TileChecker
{
	public static List<GridTile> GetMatchingTileList(GridTile targetTile)
	{
		List<GridTile> list = new List<GridTile>(0);
		List<GridTile> list2 = new List<GridTile>(0);
		list.Add(targetTile);
		list2.Add(targetTile);
		while (list.Count > 0)
		{
			GridTile gridTile = list[0];
			int rowIndex = gridTile.rowIndex;
			int tileIndex = gridTile.tileIndex;
			int tileValue = gridTile.tileValue;
			GridTile gridTile2 = TileFinder.FindLeft(tileIndex, rowIndex);
			GridTile gridTile3 = TileFinder.FindRight(tileIndex, rowIndex);
			GridTile gridTile4 = TileFinder.FindTop(tileIndex, rowIndex);
			GridTile gridTile5 = TileFinder.FindBottom(tileIndex, rowIndex);
			if (gridTile2 != null && gridTile2.tileValue == tileValue && !list2.Contains(gridTile2))
			{
				list.Add(gridTile2);
				list2.Add(gridTile2);
				gridTile2.SetFindDirection(GridTile.FindDirection.left);
			}
			if (gridTile3 != null && gridTile3.tileValue == tileValue && !list2.Contains(gridTile3))
			{
				list.Add(gridTile3);
				list2.Add(gridTile3);
				gridTile3.SetFindDirection(GridTile.FindDirection.right);
			}
			if (gridTile4 != null && gridTile4.tileValue == tileValue && !list2.Contains(gridTile4))
			{
				list.Add(gridTile4);
				list2.Add(gridTile4);
				gridTile4.SetFindDirection(GridTile.FindDirection.top);
			}
			if (gridTile5 != null && gridTile5.tileValue == tileValue && !list2.Contains(gridTile5))
			{
				list.Add(gridTile5);
				list2.Add(gridTile5);
				gridTile5.SetFindDirection(GridTile.FindDirection.bottom);
			}
			list.RemoveAt(0);
		}
		return list2;
	}
}
