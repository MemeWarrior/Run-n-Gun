using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    public TileBase floorTile, wallTileTop; //Change to array of tiles later

    public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        paintTiles(floorPos, floorTilemap, floorTile);
    }

    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePos, tile);
    }

    internal void PaintSingleBasicWall(Vector2Int pos)
    {
      paintSingleTile(wallTilemap, wallTileTop, pos);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
