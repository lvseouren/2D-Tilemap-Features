using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDataManager:MonoBehaviour
{
    public Grid grid;
    public Tilemap groundTileMap;

    static TilemapDataManager instance;

    private void Start()
    {
        instance = this;
    }

    public static TilemapDataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new TilemapDataManager();
            return instance;
        }
    }

    public TileBase GetTile(Vector3 pos)
    {
        Vector3Int position = grid.WorldToCell(pos);
        return groundTileMap.GetTile(position);
    }

    public void ClearTile(Vector3Int position)
    {
        groundTileMap.SetTile(position, null);
    }

    public void ClearTile(Vector3 pos)
    {
        Vector3Int position = grid.WorldToCell(pos);
        groundTileMap.SetTile(position, null);
    }
}