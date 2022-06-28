using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallHandler
{

  public static void createWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer TileMapVisualizer)
  {
    var basicWallPos = FindWallsInDirections(floorPos, direction2D.cardinalDirectionsList);
    foreach (var pos in basicWallPos)
    {
      TileMapVisualizer.PaintSingleBasicWall(pos);
    }
  }


  private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
  {
    HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
    foreach (var position in floorPos)
    {
      foreach (var direction in directionList)
      {
        var neighborPos = position + direction;
        if(!floorPos.Contains(neighborPos))
          wallPos.Add(neighborPos);
      }
    }
    return wallPos;
  }
}
