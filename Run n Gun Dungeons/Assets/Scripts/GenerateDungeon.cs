using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateDungeon : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;
    [SerializeField]
    private TileMapVisualizer TileMapVisualizer;

    public void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        TileMapVisualizer.Clear();
        foreach(var position in floorPos)
        {
            TileMapVisualizer.paintFloorTiles(floorPos);
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        for (int i = 0; i<iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos,walkLength);
            floorPos.UnionWith(path);

            if(startRandomlyEachIteration)
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
        }
        return floorPos;
    }   
    
}