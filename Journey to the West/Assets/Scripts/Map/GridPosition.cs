using System;
using UnityEngine;

public class GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }



    public bool Equals(object obj)
    {
        return obj is GridPosition position &&
                      x == position.x &&
                      z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}
