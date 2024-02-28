using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using UnityEngine;

namespace DungeonMaster2D
{
  internal static class Dungeon2DUtils
  {
    public static int GetValidNodes(this Node[] nodeCollection)
    {
      return nodeCollection.GetValidNodes(out _);
    }

    public static int GetValidNodes(this Node[] nodeCollection, out Node[] validNodes)
    {
      List<Node> validList = new();

      foreach (Node node in nodeCollection)
      {
        if (node != null && node.IsRoom)
          validList.Add(node);
      }
      validNodes = validList.ToArray();
      return validList.Count;
    }
  }
}