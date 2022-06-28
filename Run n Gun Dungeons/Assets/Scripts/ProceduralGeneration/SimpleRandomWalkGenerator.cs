using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SRWData randWalkParams;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randWalkParams, startPos);
        TileMapVisualizer.Clear();
        TileMapVisualizer.paintFloorTiles(floorPos);
        WallHandler.createWalls(floorPos,TileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SRWData parameters, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        for (int i = 0; i<parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos,parameters.walkLength);
            floorPos.UnionWith(path);

            if(parameters.startRandomlyEachIteration)
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
        }
        return floorPos;
    }

}
