using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private SRWData randWalkParams;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        TileMapVisualizer.Clear();
        TileMapVisualizer.paintFloorTiles(floorPos);
        WallHandler.createWalls(floorPos, TileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        for (int i = 0; i < randWalkParams.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos,randWalkParams.walkLength);
            floorPos.UnionWith(path);

            if(randWalkParams.startRandomlyEachIteration)
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
        }
        return floorPos;
    }

}
