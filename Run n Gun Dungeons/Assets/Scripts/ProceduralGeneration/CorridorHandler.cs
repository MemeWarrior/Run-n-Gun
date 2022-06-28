using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorHandler : SimpleRandomWalkGenerator
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

    CreateCorridors(floorPos);

    TileMapVisualizer.paintFloorTiles(floorPos);
    WallHandler.createWalls(floorPos, TileMapVisualizer);
  }

  private void CreateCorridors(HashSet<Vector2Int> floorPos)
  {
    var currentPos = startPos;

    for (int i = 0; i < corCount; i++)
    {
      var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPos, corLength);
      currentPos = corridor[corridor.Count - 1];
      floorPos.UnionWith(corridor);
    }
  }
}
