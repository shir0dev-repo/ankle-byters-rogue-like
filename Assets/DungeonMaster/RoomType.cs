using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonMaster2D
{
    public enum NodeType
    {
        Basic,
        Boss,
        Treasure,
        Sanctuary,
        SanctuaryOld,
        Secret
    }

    public static class NodeTypeUtils
    {
        public static NodeType[] GetSpecialRoomTypes()
        {
            List<NodeType> nodes = new() { NodeType.Boss, NodeType.Basic, NodeType.Secret };
            return GetSpecialRoomTypes(nodes);
        }
        public static NodeType[] GetSpecialRoomTypes(IEnumerable<NodeType> ignoreTypes)
        {
            List<NodeType> roomTypes = new List<NodeType>(Enum.GetValues(typeof(NodeType)).Cast<NodeType>().ToArray());

            foreach (NodeType type in ignoreTypes)
            {
                roomTypes.Remove(type);
            }

            return roomTypes.ToArray();
        }
    }
}