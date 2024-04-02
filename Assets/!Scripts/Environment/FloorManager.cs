using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonMaster2D;
using UnityEngine;

using Random = UnityEngine.Random;

public class FloorManager : Singleton<FloorManager>
{
    public static Action<Room, Door> OnRoomEntered;
    public static Action<Room> OnRoomCleared;
    public static Action<Door> OnDoorCollision;

    [SerializeField] private Material _fogMaterial;
    [SerializeField] private DungeonGeneratorData _generatorData;
    [SerializeField] private EntranceRoomDictionary[] _entrancePrefabDictionaries;
    [SerializeField] private Dungeon2D _dungeon;

    private readonly Dictionary<Node, Room> _nodeRoomDictionary = new();

    public Action OnMapGenerated { get; set; }
    public Room CurrentRoom { get; private set; }
    public Room[] Rooms => _nodeRoomDictionary.Values.ToArray();

    private void OnEnable()
    {
        OnDoorCollision += EnterRoom;
        OnRoomCleared += ClearRoom;
        InsanityManager.OnInsanityChanged += SetFogValue;
    }
    public Node GetNode(Room room)
    {
        return _nodeRoomDictionary.FirstOrDefault(x => x.Value == room).Key;
    }
    private void SetFogValue(int newInsanity)
    {
        _fogMaterial.SetFloat("_Opacity", Mathf.Lerp(0.15f, 0.6f, newInsanity / 100f));
    }

    protected override void Awake()
    {
        base.Awake();
        GenerateDungeon();
    }

    private void Start()
    {
        _nodeRoomDictionary[_dungeon.StartingNode].UnlockRoom();
    }

    private void OnDisable()
    {
        OnDoorCollision -= EnterRoom;
        OnRoomCleared -= ClearRoom;
    }

    [ContextMenu("Generate")]
    public void GenerateDungeon()
    {
        _fogMaterial.SetFloat("_Opacity", 0.15f);
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
    }

    [ContextMenu("Lock Dungeon")]
    public void LockDungeon()
    {
        foreach (Room room in _nodeRoomDictionary.Values)
            room.LockRoom();
    }

    [ContextMenu("Unlock Dungeon")]
    public void UnlockDungeon()
    {
        foreach (Room room in _nodeRoomDictionary.Values)
            room.UnlockRoom();
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

    private void EnterRoom(Door door)
    {
        if (!CurrentRoom.DoorRoomPositionDictionary.Keys.Contains(door)) return;
        Vector3 enteredRoomPosition = CurrentRoom.DoorRoomPositionDictionary[door];

        Room enteredRoom = _nodeRoomDictionary.Values.FirstOrDefault(r => r.transform.position == enteredRoomPosition);

        if (enteredRoom == null)
            throw new InvalidOperationException("Entered room does not exist!");

        enteredRoom.Enter(door);
        CurrentRoom = enteredRoom;


        if (CurrentRoom.Cleared)
            ClearRoom(CurrentRoom);
    }

    private void ClearRoom(Room room)
    {
        room.UnlockRoom();
    }
}
