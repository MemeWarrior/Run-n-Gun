using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Random;
using System.Linq;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    public TileBase floorTile, wallTileTop; //Change to array of tiles later
    public List<GameObject> objects;
    public List<GameObject> enemies;
    public GameObject spawn;
    private List<Vector2Int> objectLocations = new List<Vector2Int>();

    private float enemyDensity = 0.01f;
    private float objectDensity = 0.05f;


    public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        paintTiles(floorPos, floorTilemap, floorTile);

        List<Vector2Int> spawnPoint = floorPos.OrderBy( x => Random.value ).Take(1).ToList();
        setSpawn(spawnPoint);

        //Place objects
        int objectCount = Mathf.RoundToInt(floorPos.Count()*objectDensity);
        // Debug.Log("Tiles: " + floorPos.Count());
        // Debug.Log("Objects: "+ objectCount);

        List<Vector2Int> objectsToCreate = floorPos.OrderBy( x => Random.value ).Take(objectCount).ToList();

        placeObjects(objectsToCreate);

        //Place enemies
        int enemiesCount = Mathf.RoundToInt(floorPos.Count()*enemyDensity);
        // Debug.Log("Tiles: " + floorPos.Count());
        // Debug.Log("Objects: "+ objectCount);

        List<Vector2Int> enemiesToCreate = floorPos.OrderBy( x => Random.value ).Take(objectCount).ToList();
        placeEnemies(enemiesToCreate);
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

    private void placeObjects(List<Vector2Int> objectsToCreate)
    {
        foreach (var objectPosition in objectsToCreate)
        {
            if(!objectLocations.Contains(objectPosition))
            {
                int index = Random.Range(0, objects.Count);
                Instantiate (objects[index], new Vector3(objectPosition.x, objectPosition.y), objects[index].transform.rotation);
                objectLocations.Add(objectPosition);
            }
        }
    }

    private void placeEnemies(List<Vector2Int> enemiesToCreate)
    {
        foreach (var enemyPosition in enemiesToCreate)
        {
            if(!objectLocations.Contains(enemyPosition))
            {
                int index = Random.Range(0, enemies.Count);
                Instantiate (enemies[index], new Vector3(enemyPosition.x, enemyPosition.y), enemies[index].transform.rotation);
            }
        }
    }

    private void setSpawn(List<Vector2Int> spawnPoint)
    {
        foreach (var position in spawnPoint)
        {
            Instantiate (spawn, new Vector3(position.x, position.y), spawn.transform.rotation);
            objectLocations.Add(position);
        }
    }

    public void Clear()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject spawn = GameObject.FindWithTag("Spawn");
        for(int i=0; i< objects.Length; i++)
         {
             Destroy(objects[i]);
             DestroyImmediate(objects[i]);
         }
         for(int i=0; i< enemies.Length; i++)
          {
              Destroy(enemies[i]);
              DestroyImmediate(enemies[i]);
          }
          Destroy(spawn);

        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

}
