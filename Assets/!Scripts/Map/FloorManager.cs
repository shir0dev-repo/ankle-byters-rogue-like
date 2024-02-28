using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonMaster2D;
using UnityEngine;

public class FloorManager : Singleton<FloorManager>
{
    [SerializeField] private DungeonGeneratorData _generatorData;
    [SerializeField] private EntranceRoomDictionary[] _entrancePrefabDictionaries;
    [SerializeField] private GameObject _startingRoomPrefab;
    [SerializeField] private Dungeon2D _dungeon;

    private readonly Dictionary<Node, Room> _nodeRoomDictionary = new();

    public static event EventHandler<RoomArgs> OnRoomInteractedWith;
    public Action OnMapGenerated { get; set; }
    public Room CurrentRoom { get; private set; }

    protected override void Awake()
    {
        GenerateDungeon();
    }

    [ContextMenu("Generate")]
    public void GenerateDungeon()
    {
        foreach (Room room in _nodeRoomDictionary.Values)
            Destroy(room.gameObject);
        _nodeRoomDictionary.Clear();

        _generatorData.GetRandomSeed();

        _dungeon = MapGenerator.Generate2D(_generatorData, out System.Random random);

        foreach (Node node in _dungeon.ValidNodes)
        {
            InitializeRoom(node, random);
        }

        CurrentRoom = _nodeRoomDictionary[_dungeon.StartingNode];

        OnMapGenerated?.Invoke();

        OnRoomInteractedWith?.Invoke(this, new()
        {
            Room = CurrentRoom,
            InteractionType = RoomInteractionType.Entered | RoomInteractionType.Cleared
        });

        OnRoomInteractedWith?.Invoke(this, new()
        {
            Room = _nodeRoomDictionary[_dungeon.ValidNodes[^1]],
            InteractionType = RoomInteractionType.Entered | RoomInteractionType.Cleared
        });
    }

    private GameObject InitializeRoom(Node node, System.Random random)
    {
        EntranceRoomDictionary dict = _entrancePrefabDictionaries.FirstOrDefault(e => e.Entrances == node.Entrances);
        GameObject go;
        if ((int)node.Entrances == 15)
            dict = _entrancePrefabDictionaries[0];

        try
        {
            go = dict.ValidRoomPrefabs[random.Next(0, dict.ValidRoomPrefabs.Length)];
        }
        catch (Exception)
        {
            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Debug.LogWarning($"Room {node} did not have a valid room prefab!");
        }

        GameObject roomObj = Instantiate(go, transform);
        roomObj.name = node.ToString();
        Room r = roomObj.AddComponent<Room>();
        r.Init(node, _dungeon.StartingNode);
        _nodeRoomDictionary.Add(node, r);

        return go;
    }
}
