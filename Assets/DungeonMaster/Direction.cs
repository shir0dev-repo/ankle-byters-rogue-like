using UnityEngine;

namespace DungeonMaster2D
{
    [System.Flags]
    public enum Direction
    {
        Left = 1,
        Up = 2,
        Right = 4,
        Down = 8
    }

    public static class DirectionUtils
    {
        public static Vector2Int GetVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector2Int.left,
                Direction.Up => Vector2Int.up,
                Direction.Right => Vector2Int.right,
                Direction.Down => Vector2Int.down,
                _ => default
            };
        }

        public static void SetNeighbourDirections(this Node origin, Node[] neighbours)
        {
            Direction direction = 0;
            for (int i = 0, d = 1; i < neighbours.Length; i++, d *= 2)
            {
                if (neighbours[i] != null && neighbours[i].IsRoom)
                {
                    direction = (Direction)d | direction;
                }
            }

            origin.Entrances = direction;
        }
    }
}