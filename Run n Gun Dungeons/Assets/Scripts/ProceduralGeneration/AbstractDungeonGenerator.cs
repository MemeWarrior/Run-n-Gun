using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualizer TileMapVisualizer = null;

    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        TileMapVisualizer.Clear();

        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
