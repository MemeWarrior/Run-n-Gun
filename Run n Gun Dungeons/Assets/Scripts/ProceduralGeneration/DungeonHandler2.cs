using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonHandler2 : SimpleRandomWalkGenerator
{
  [SerializeField]
  private int corLength = 14, corCount = 5;
  [SerializeField]
  [Range(0.1f,1f)]
  private float roomPercent = 0.8f;

  [SerializeField]
  public SRWData roomGenerationParams;

  protected override void RunProceduralGeneration()
  {
    CorridorFirstGeneration();
  }

  private void CorridorFirstGeneration()
  {
    HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
    HashSet<Vector2Int> potRoomPos = new HashSet<Vector2Int>();

    CreateCorridors(floorPos, potRoomPos);

    HashSet<Vector2Int> roomPos = CreateRooms(potRoomPos);

    floorPos.UnionWith(roomPos);

    TileMapVisualizer.paintFloorTiles(floorPos);
    WallHandler.createWalls(floorPos, TileMapVisualizer);
  }

  private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potRoomPos)
  {
    HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
    int roomCount = Mathf.RoundToInt(potRoomPos.Count*roomPercent);

    List<Vector2Int> roomsToCreate = potRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();
    foreach (var roomPosition in roomsToCreate)
    {
      var roomFloor = RunRandomWalk(randWalkParams, roomPosition);
      roomPos.UnionWith(roomFloor);
    }
    return roomPos;
  }

  private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potRoomPos)
  {
    var currentPos = startPos;
    potRoomPos.Add(currentPos);

    for (int i = 0; i < corCount; i++)
    {
      var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPos, corLength);
      currentPos = corridor[corridor.Count - 1];
      potRoomPos.Add(currentPos);
      floorPos.UnionWith(corridor);
    }
  }
}
