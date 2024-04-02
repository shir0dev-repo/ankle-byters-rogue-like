using System;
using System.Collections;
using System.Collections.Generic;
using DungeonMaster2D;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{
    private const int _ROOM_PIXEL_SIZE = 80;
    private const int _ROOM_BORDER_PIXEL_SIZE = 5;
    private Texture2D _mapGridTexture;
    [SerializeField] private RawImage _mapImage;

    private void Start()
    {
        UpdateMapUI();
    }

    public void UpdateMapUI()
    {
        Room[] rooms = FloorManager.Instance.Rooms;
        int textureResolution = (_ROOM_PIXEL_SIZE * 9) + (_ROOM_BORDER_PIXEL_SIZE * 11);
        _mapGridTexture = new Texture2D(textureResolution, textureResolution);
        PaintGridBorders(ref _mapGridTexture);
        PaintRooms(ref _mapGridTexture, rooms, FloorManager.Instance.GetNode(FloorManager.Instance.CurrentRoom).Position);
        _mapImage.texture = _mapGridTexture;

    }

    private void PaintGridBorders(ref Texture2D texture)
    {
        int stride = _ROOM_PIXEL_SIZE + _ROOM_BORDER_PIXEL_SIZE;

        Color32[] c = new Color32[_ROOM_BORDER_PIXEL_SIZE * texture.width - 1];
        Array.Fill(c, Color.black);

        for (int x = 0; x < texture.width; x += stride)
        {
            texture.SetPixels32(x, 0, _ROOM_BORDER_PIXEL_SIZE, texture.height - 1, c);
        }
        for (int y = 0; y < texture.height; y += stride)
        {
            texture.SetPixels32(0, y, texture.width - 1, _ROOM_BORDER_PIXEL_SIZE, c);
        }

        texture.Apply();
    }

    private void PaintRooms(ref Texture2D texture, Room[] rooms, Vector2Int offset)
    {
        Color32[] c = new Color32[_ROOM_PIXEL_SIZE * _ROOM_PIXEL_SIZE];


        foreach (Room room in rooms)
        {
            Color roomColor;
            float roomAlpha = 1;

            if (room.Cleared == false)
                roomAlpha = 0.5f;
            if (room.Visible == false)
                roomColor = Color.black;
            else
            {
                roomColor = room.Node.NodeType switch
                {
                    NodeType.Basic => Color.gray,
                    NodeType.Boss => Color.red,
                    _ => Color.gray
                };
            }

            roomColor.a = roomAlpha;
            Array.Fill(c, roomColor);
            Vector2Int coord = GetScaledRoomCoordinate(room.Node);

            texture.SetPixels32(coord.x, coord.y, _ROOM_PIXEL_SIZE - _ROOM_BORDER_PIXEL_SIZE, _ROOM_PIXEL_SIZE - _ROOM_BORDER_PIXEL_SIZE, c);
        }

        texture.Apply();
    }

    private Vector2Int GetScaledRoomCoordinate(Node n)
    {
        return new Vector2Int()
        {
            x = ((n.x + 1) * _ROOM_BORDER_PIXEL_SIZE) + (n.x * _ROOM_PIXEL_SIZE),
            y = ((n.y + 1) * _ROOM_BORDER_PIXEL_SIZE) + (n.y * _ROOM_PIXEL_SIZE)
        };
    }
}
