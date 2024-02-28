using System;
using System.Collections;
using System.Collections.Generic;
using DungeonMaster2D;
using UnityEngine;

[SelectionBase]
public class Room : MonoBehaviour
{
    private const float _ROOM_SCALE_X = 17.98f;
    private const float _ROOM_SCALE_Y = 10.0f;
    public bool Visible { get; private set; }
    public Node Node { get; private set; }

    private void OnEnable()
    {
        FloorManager.OnRoomInteractedWith += HandleRoomInteraction;
    }

    private void OnDisable()
    {
        FloorManager.OnRoomInteractedWith -= HandleRoomInteraction;
    }

    public void Init(Node node, Node startingNode)
    {
        Node = node;
        Visible = false;


        Vector3 pos = node - startingNode;
        pos.x *= _ROOM_SCALE_X;
        pos.y *= _ROOM_SCALE_Y;
        pos.z = 0f;

        transform.position = pos;
    }

    public void HandleRoomInteraction(object sender, RoomArgs args)
    {
        if (args.Room != this) return;

        if (args.InteractionType.Contains(RoomInteractionType.Entered))
            Debug.Log("Room entered! " + Node.ToString());
        if (args.InteractionType.Contains(RoomInteractionType.Cleared))
            Debug.Log("Room cleared! " + Node.ToString());
    }
}
