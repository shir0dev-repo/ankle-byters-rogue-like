using System;
using System.Collections;
using System.Collections.Generic;
using DungeonMaster2D;
using UnityEngine;

public class FloorManager : Singleton<FloorManager>
{
    [SerializeField] private DungeonGeneratorData _generatorData;
    [SerializeField] private Dungeon2D _dungeon;
    public Action OnMapGenerated;

    protected override void Awake()
    {
        _generatorData.GetRandomSeed();

        _dungeon = MapGenerator.Generate2D(_generatorData);
        Debug.Log(_dungeon.ValidNodes.Length);

        foreach (Node node in _dungeon.ValidNodes)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Room r = go.AddComponent<Room>();
            r.Init(node, _dungeon.StartingNode);
        }
    }
}
