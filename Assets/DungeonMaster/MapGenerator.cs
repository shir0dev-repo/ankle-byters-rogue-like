using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace DungeonMaster2D
{
    public static class MapGenerator
    {
        public static Dungeon2D Generate2D(DungeonGeneratorData data)
        {
            // initialize seeded random
            Random random = new Random(data.GetSeed());

            // generate new dungeon
            Dungeon2D dungeon = Generate2DInitial(data, random, out int roomCount);

            // dungeon generation completed before iterations begin
            if (roomCount >= dungeon.MaxNodes && dungeon.GetDeadends().Length > 3)
            {
                dungeon.AssignEntrances();
                dungeon.AssignSpecialNodes(random);

                if (dungeon.AddSecretNode())
                {
                    Debug.Log(GetFinalString(0, data.CurrentSeed, dungeon));
                    return dungeon;
                }
            }

            // iterate through dungeon, creating a new dungeon if current iteration does not suffice
            int currentIteration = 0;
            while (currentIteration < 30)
            {
                Queue<Node> deadendQueue = new Queue<Node>(dungeon.GetDeadends(excludeStartingRoom: false));

                // iterate through deadends, enqueueing any new rooms created
                int attempts = 0;
                while (attempts < 300 && roomCount < dungeon.MaxNodes && deadendQueue.TryDequeue(out Node currentNode))
                {
                    Node[] neighbours = dungeon.GetPseudoNeighbours(currentNode);
                    foreach (Node neighbour in neighbours)
                    {
                        if (dungeon.Exists(neighbour)) continue;
                        else if (neighbour.Validate(dungeon, random.Next(100)))
                        {
                            if (dungeon.AddNode(neighbour))
                            {
                                deadendQueue.Enqueue(neighbour);
                                roomCount++;
                            }
                        }
                    }
                }

                if (roomCount >= dungeon.MinNodes && dungeon.GetDeadends().Length > 3)
                {
                    dungeon.AssignEntrances();
                    dungeon.AssignSpecialNodes(random);

                    if (dungeon.AddSecretNode())
                    {
                        Debug.Log(GetFinalString(currentIteration, data.CurrentSeed, dungeon));

                        return dungeon;
                    }
                }
                else
                {
                    if (!data.UseRandomSeed)
                        Debug.LogWarning($"Seed {data.CurrentSeed} could not generate valid dungeon. Defaulting to random seed generation.");

                    random = new Random(data.GetRandomSeed());
                    dungeon = Generate2DInitial(data, random, out roomCount);
                    currentIteration++;
                }
            }

            throw new StackOverflowException($"Dungeon could not generate after {currentIteration} attempts.");
        }

        private static string GetFinalString(int currentIteration, int seed, Dungeon2D dungeon)
        {
            return $"Dungeon generated successfully. See console message for more info: " + '\n' +
                            $"Attempts: {currentIteration + 1}" + '\n' +
                            $"Seed: {seed}" + '\n' +
                            $"Dungeon stats:\n{dungeon}";
        }

        private static Dungeon2D Generate2DInitial(DungeonGeneratorData data, Random random, out int roomCount)
        {
            Node startingRoom = new Node(new(4, 4), true);
            Dungeon2D dungeon = new Dungeon2D(data, startingRoom);

            Stack<Node> roomStack = new Stack<Node>();

            roomStack.PushRange(dungeon.GetPseudoNeighbours(startingRoom));

            roomCount = 1;
            int attempts = 0;
            int maxIterations = 150;
            int nextRand;


            while (attempts < maxIterations && roomCount < dungeon.MaxNodes)
            {
                attempts++;

                if (!roomStack.TryPop(out Node currentNode))
                    break;

                nextRand = random.Next(100);

                if (!currentNode.Validate(dungeon, nextRand))
                    continue;

                if (!dungeon.AddNode(currentNode))
                    continue;

                roomCount++;
                roomStack.PushRange(dungeon.GetPseudoNeighbours(currentNode));
            }

            return dungeon;
        }

        private static void PushRange<T>(this Stack<T> stack, T[] items)
        {
            for (int i = items.Length - 1; i >= 0; i--)
            {
                stack.Push(items[i]);
            }
        }

        public static void AssignSpecialNodes(this Dungeon2D dungeon, Random random)
        {
            // ordering by distance (descending) and assigning [0] to the boss room ensures it is always the farthest room from the start
            List<Node> deadends = dungeon.GetDeadends().OrderByDescending(node => Node.Distance(dungeon.StartingNode, node)).ToList();
            deadends[0].NodeType = NodeType.Boss;
            deadends.RemoveAt(0);

            List<NodeType> remainingTypes = NodeTypeUtils.GetSpecialRoomTypes().ToList();
            foreach (Node node in deadends)
            {
                if (remainingTypes.Count <= 0) break;

                int randIndex = random.Next(0, remainingTypes.Count);
                node.NodeType = remainingTypes[randIndex];
                remainingTypes.RemoveAt(randIndex);
            }
        }
    }
}