using System.Collections;
using System.Collections.Generic;
using DungeonMaster2D;
using UnityEngine;

public class Room : MonoBehaviour
{
    private const float _ROOM_SCALE_X = 17.98f;
    private const float _ROOM_SCALE_Y = 10.0f;
    public bool Visible { get; private set; }
    public Node Node { get; private set; }

    public void Init(Node node, Node startingNode)
    {
        Node = node;
        Visible = false;

        transform.localScale = new Vector3(_ROOM_SCALE_X, _ROOM_SCALE_Y, 1);
        Vector3 pos = node - startingNode;
        pos.x *= _ROOM_SCALE_X;
        pos.y *= _ROOM_SCALE_Y;
        pos.z = 0f;
        if (node != startingNode)
            GetComponent<MeshRenderer>().material.color = node.NodeType switch
            {
                NodeType.Basic => Color.white,
                NodeType.Boss => Color.red,
                NodeType.Treasure => Color.yellow,
                NodeType.Sanctuary => Color.green,
                NodeType.SanctuaryOld => Color.blue,
                NodeType.Secret => new Color(0.3f, 0.3f, 0.3f, 1),
                _ => Color.white
            };
        else
            GetComponent<MeshRenderer>().material.color = Color.cyan;

        transform.position = pos;
    }
}
