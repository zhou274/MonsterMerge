using System;

public class DistancedTile
{
	public GridTile tile;

	public float distance;

	public void Create(GridTile t, float d)
	{
		this.tile = t;
		this.distance = d;
	}
}
