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

    public List<TileBase> floorTiles = new List<TileBase>();
    public List<TileBase> wallTile = new List<TileBase>();
    public List<GameObject> objects;
    public List<GameObject> enemies;
    public GameObject spawn;
    public GameObject portal;
    private List<Vector2Int> objectLocations = new List<Vector2Int>();
    private List<Vector2Int> wallLocations = new List<Vector2Int>();

    private float enemyDensity = 0.015f;
    private float objectDensity = 0.1f;


    public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        paintTiles(floorPos, floorTilemap, floorTiles);

        List<Vector2Int> spawnPoint = floorPos.OrderBy( x => Random.value ).Take(1).ToList();
        setSpawn(spawnPoint);

        List<Vector2Int> portalPoint = floorPos.OrderBy( x => Random.value ).Take(1).ToList();
        makePortal(portalPoint);

        //Place objects
        int objectCount = Mathf.RoundToInt(floorPos.Count()*objectDensity);
        // Debug.Log("Tiles: " + floorPos.Count());
        // Debug.Log("Objects: "+ objectCount);

        List<Vector2Int> objectsToCreate = floorPos.OrderBy( x => Random.value ).Take(objectCount).ToList();

        placeObjects(objectsToCreate);

        //Place enemies
        int enemiesCount = Mathf.RoundToInt(floorPos.Count()*enemyDensity);
        Debug.Log("Tiles: " + floorPos.Count());
        Debug.Log("Snakes: "+ enemiesCount);

        List<Vector2Int> enemiesToCreate = floorPos.OrderBy( x => Random.value ).Take(enemiesCount).ToList();
        placeEnemies(enemiesToCreate);
    }

    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    private void paintSingleTile(Tilemap tilemap, List<TileBase> tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        int index = Random.Range(0, tile.Count - 1);
        tilemap.SetTile(tilePos, tile[index]);
    }

    internal void PaintSingleBasicWall(Vector2Int pos)
    {
      paintSingleTile(wallTilemap, wallTile, pos);
      wallLocations.Add(pos);
    }

    private void placeObjects(List<Vector2Int> objectsToCreate)
    {
        foreach (var objectPosition in objectsToCreate)
        {
            if(!objectLocations.Contains(objectPosition) && !wallLocations.Contains(objectPosition))
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
            if(!objectLocations.Contains(enemyPosition) && !wallLocations.Contains(enemyPosition))
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
            if(!wallLocations.Contains(position))
            {
                Instantiate (spawn, new Vector3(position.x, position.y), spawn.transform.rotation);
                objectLocations.Add(position);
            }
        }
    }

    private void makePortal(List<Vector2Int> portalPoint)
    {
        foreach (var position in portalPoint)
        {
            if(!wallLocations.Contains(position))
            {
                Instantiate (portal, new Vector3(position.x, position.y), spawn.transform.rotation);
                objectLocations.Add(position);
            }
        }
    }

    public void Clear()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject spawn = GameObject.FindWithTag("Spawn");
        GameObject portal = GameObject.FindWithTag("Portal");
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
          Destroy(portal);

        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

}
