using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i<walkLength; i++)
        {
            var newPos = previousPos + direction2D.getRandomCardinalDirection();
            path.Add(newPos);
            previousPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
      List<Vector2Int> corridor = new List<Vector2Int>();
      var direction = direction2D.getRandomCardinalDirection();
      var currentPos = startPos;
      corridor.Add(currentPos);

      for (int i = 0; i < corridorLength; i++)
      {
        currentPos += direction;
        corridor.Add(currentPos);
      }
      return corridor;
    }

}

public static class direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //Up
        new Vector2Int(1,0), //Down
        new Vector2Int(0,-1), //Right
        new Vector2Int(-1,0) //Left
    };

    public static Vector2Int getRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];
    }
}
