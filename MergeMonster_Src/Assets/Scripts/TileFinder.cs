using System;

public static class TileFinder
{
	public static GridTile FindLeft(int TileIndex, int RowIndex)
	{
		if (TileIndex == 0)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex].tiles[TileIndex - 1];
	}

	public static GridTile FindRight(int TileIndex, int RowIndex)
	{
		if (TileIndex == 4)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex].tiles[TileIndex + 1];
	}

	public static GridTile FindTop(int TileIndex, int RowIndex)
	{
		if (RowIndex == 0)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex - 1].tiles[TileIndex];
	}

	public static GridTile FindBottom(int TileIndex, int RowIndex)
	{
		if (RowIndex == 4)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex + 1].tiles[TileIndex];
	}

	public static GridTile FindTopLeft(int TileIndex, int RowIndex)
	{
		if (TileIndex == 0 || RowIndex == 0)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex - 1].tiles[TileIndex - 1];
	}

	public static GridTile FindTopRight(int TileIndex, int RowIndex)
	{
		if (TileIndex == 4 || RowIndex == 0)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex - 1].tiles[TileIndex + 1];
	}

	public static GridTile FindBottomLeft(int TileIndex, int RowIndex)
	{
		if (TileIndex == 0 || RowIndex == 4)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex + 1].tiles[TileIndex - 1];
	}

	public static GridTile FindBottomRight(int TileIndex, int RowIndex)
	{
		if (TileIndex == 4 || RowIndex == 4)
		{
			return null;
		}
		return GameManager.instance.gridRowsContainer.gridRows[RowIndex + 1].tiles[TileIndex + 1];
	}
}
