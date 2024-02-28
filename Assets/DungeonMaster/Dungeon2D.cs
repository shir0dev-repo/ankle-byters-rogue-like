using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

namespace DungeonMaster2D
{

    [Serializable]
    public class Dungeon2D : IEnumerable
    {
        public const int DIMENSIONS = 9;
        public const int MAX_SIZE = 81;

        [SerializeField] protected Node[] _nodes;
        protected int _nodeCount = 0;

        public int MaxNodes { get; private set; }
        public int MinNodes => MaxNodes - 3;

        public Node StartingNode
        {
            get { return _nodes[40]; }
        }

        public Node[] ValidNodes
        {
            get
            {
                Dungeon2DUtils.GetValidNodes(this, out Node[] validNodes);
                return validNodes;
            }
        }

        public Dungeon2D(DungeonGeneratorData data, Node startingNode)
        {
            _nodeCount = 0;
            _nodes = new Node[MAX_SIZE];
            MaxNodes = data.TargetRoomCount;

            AddNode(startingNode);
        }

        public Node this[int i]
        {
            get
            {
                try
                {
                    return _nodes[i];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
            private set
            {
                try
                {
                    _nodes[i] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static implicit operator Node[](Dungeon2D dungeon) => dungeon._nodes;

        public Dungeon2DEnumerator GetEnumerator() => new(_nodes);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool InRange(Node node)
            => 0 <= node.x && node.x < DIMENSIONS &&
                0 <= node.y && node.y < DIMENSIONS;

        public bool Exists(Node node)
            => InRange(node) &&
                this[node.Index] != null &&
                this[node.Index].IsRoom;

        public bool AddNode(Node node)
        {
            if (_nodeCount + 1 <= MAX_SIZE && InRange(node))
            {
                this[node.Index] = node;
                _nodeCount++;
                return true;
            }

            else return false;
        }

        public void AssignEntrances()
        {
            foreach (Node node in ValidNodes)
            {
                node.SetNeighbourDirections(GetExistingNeighbours(node));
            }
        }

        public Node[] GetPseudoNeighbours(Node origin)
        {
            Node[] neighbours = new Node[4];

            for (int i = 0; i < 4; i++)
            {
                neighbours[i] = origin + Node.directions[i];
            }

            return neighbours;
        }

        public Node[] GetExistingNeighbours(Node origin)
        {
            Node[] pseudoNeighbours = GetPseudoNeighbours(origin);
            Node[] existingNeighbours = new Node[4];

            for (int i = 0; i < 4; i++)
            {
                if (Exists(pseudoNeighbours[i]))
                    existingNeighbours[i] = this[pseudoNeighbours[i].Index];
            }

            return existingNeighbours;
        }

        public Node[] GetDeadends(bool excludeStartingRoom = true)
        {
            List<Node> deadends = new();

            foreach (Node node in ValidNodes)
            {
                if (excludeStartingRoom && node == StartingNode)
                    continue;
                else if (GetExistingNeighbours(node).GetValidNodes() > 1)
                    continue;

                deadends.Add(node);
            }

            return deadends.ToArray();
        }

        public Node[] GetValidSecretNodePositions(int minNeighboursRequired)
        {
            List<Node> validPositions = new();
            // iterate through existing nodes
            foreach (Node existingNode in ValidNodes)
            {
                // find neighbouring positions
                Node[] pseudoNeighbours = GetPseudoNeighbours(existingNode);

                // iterate through positions
                foreach (Node pseudoNeighbour in pseudoNeighbours)
                {
                    // check if position is both in range, and does not contain existing node
                    if (!InRange(pseudoNeighbour)) continue;
                    else if (Exists(pseudoNeighbour)) continue;

                    // find adjacent existing nodes
                    Node[] existingNeighbours = GetExistingNeighbours(pseudoNeighbour);

                    // see if existing neighbour count passes minimum required
                    if (existingNeighbours.GetValidNodes() < minNeighboursRequired) continue;
                    // check if any adjacent rooms are the boss room
                    else if (existingNeighbours.Any(n => n != null && n.NodeType == NodeType.Boss)) continue;

                    // node passes checks, create new to add to list
                    Node validPosition = new(pseudoNeighbour.Position, isRoom: true)
                    {
                        NodeType = NodeType.Secret
                    };

                    // add if not already added
                    if (!validPositions.Contains(validPosition))
                        validPositions.Add(validPosition);
                }
            }

            return validPositions.ToArray();
        }

        public bool AddSecretNode(int minNeighboursRequired = 3)
        {
            minNeighboursRequired = Mathf.Clamp(minNeighboursRequired, 0, 4);

            for (int i = 0; i < minNeighboursRequired; i++)
            {
                Node[] validPositions = GetValidSecretNodePositions(minNeighboursRequired - i);
                // no valid positions
                if (validPositions.Length <= 0)
                    continue;

                Node selectedNode = validPositions[Random.Range(0, validPositions.Length)];
                selectedNode.SetNeighbourDirections(GetExistingNeighbours(selectedNode));

                AddNode(selectedNode);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Node total: {ValidNodes.Length} " + '\n' +
                   $"Target node count: {MaxNodes}";
        }
    }

    public class Dungeon2DEnumerator : IEnumerator
    {
        public Node[] nodes;
        int _position = -1;

        public Dungeon2DEnumerator(Node[] nodes) => this.nodes = nodes;

        public Node Current
        {
            get
            {
                try
                {
                    return nodes[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            _position++;
            return _position < nodes.Length;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
