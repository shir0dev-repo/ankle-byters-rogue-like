using System;
using UnityEngine;

namespace DungeonMaster2D
{
    [Serializable]
    public class Node : IEquatable<Node>
    {
        public static Node left
        {
            get { return new Node(Vector2Int.left); }
        }
        public static Node up
        {
            get { return new Node(Vector2Int.up); }
        }
        public static Node right
        {
            get { return new Node(Vector2Int.right); }
        }
        public static Node down
        {
            get { return new Node(Vector2Int.down); }
        }
        public static readonly Node[] directions = new Node[4]
        {
            left,
            up,
            right,
            down
        };

        [SerializeField] protected Vector2Int m_position;
        [SerializeField] protected Direction m_entrances;
        [SerializeField] protected NodeType m_roomType;
        [SerializeField] protected bool m_isRoom;
        protected bool m_hasBeenEvaluated;

        public Vector2Int Position
        {
            get { return m_position; }
            set { m_position = value; }
        }
        public int x
        {
            get { return m_position.x; }
        }
        public int y
        {
            get { return m_position.y; }
        }

        public int Index => (x * Dungeon2D.DIMENSIONS) + y;

        public Direction Entrances
        {
            get { return m_entrances; }
            set { m_entrances = value; }
        }

        public NodeType NodeType
        {
            get { return m_roomType; }
            set { m_roomType = value; }
        }

        public bool IsRoom
        {
            get
            {
                if (m_hasBeenEvaluated)
                    return m_isRoom;
                else
                    return false;
            }
        }
        public Node() : this(Vector2Int.zero, false) { }
        public Node(Vector2Int position) : this(position, false) { }
        public Node(Node other) : this(other.m_position, other.m_isRoom) { }
        public Node(Vector2Int position, bool isRoom)
        {
            m_position = position;
            m_hasBeenEvaluated = isRoom;
            m_isRoom = isRoom;
        }

        public bool Validate(Dungeon2D dungeon, int randomNum)
        {
            if (!dungeon.InRange(this)) m_isRoom = false;
            else if (dungeon.Exists(this)) m_isRoom = false;
            else if (dungeon.GetExistingNeighbours(this).GetValidNodes() > 1) m_isRoom = false;
            else if (randomNum < 40) m_isRoom = false;
            else m_isRoom = true;

            m_hasBeenEvaluated = true;
            return m_isRoom;
        }

        public static float Distance(Node a, Node b)
        {
            int x = a.x - b.x;
            int y = a.y - b.y;
            return Mathf.Sqrt(x * x + y * y);
        }

        public static implicit operator Vector3(Node node)
        {
            return new Vector3(node.x, node.y);
        }
        public static implicit operator Vector2Int(Node node)
        {
            return node.m_position;
        }
        public static Node operator +(Node node, Node other)
        {
            return new Node(node.m_position + other.m_position);
        }
        public static Node operator -(Node node, Node other)
        {
            return new Node(node.m_position - other.m_position);
        }
        public static bool operator ==(Node lhs, Node rhs)
        {
            if (lhs is not null && rhs is null) return false;
            else if (lhs is null && rhs is not null) return false;
            else if (lhs is null && rhs is null) return true;

            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = num * num + num2 * num2;
            return num3 < 9.99999944E-11f;
        }
        public static bool operator !=(Node lhs, Node rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return (m_position.x.GetHashCode() << 2) ^ (m_position.y.GetHashCode() >> 2);
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
        public override bool Equals(object other)
        {
            if (other is not Node)
                return false;

            return Equals(other as Node);
        }
        public bool Equals(Node other)
        {
            return other.x == x
                && other.y == y
                && other.IsRoom == IsRoom;
        }
    }
}
