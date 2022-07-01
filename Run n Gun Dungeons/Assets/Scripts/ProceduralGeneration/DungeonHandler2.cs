using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonHandler2 : SimpleRandomWalkGenerator
{
  [SerializeField]
  private int corLength = 14, corCount = 5;
  [SerializeField]
  [Range(0.1f,1f)]
  private float roomPercent = 0.8f;
  [SerializeField]
  public SRWData roomGenerationParams;
  public int enemyCount;
  private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
  private HashSet<Vector2Int> floorPos, corPos;

  public GameObject player;
  private GameObject portal;
  private PortalScript portalScript;

  void Start()
  {
      BeginNewGame();
  }

  // Update is called once per frame
  void Update()
  {
      enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

      if (enemyCount == 0)
      {
          ResetDungeon();
      }
  }

  void ResetDungeon()
  {
      portalScript = portal.GetComponent<PortalScript>();

      portal.SetActive(true);
      if(portalScript.PlayerClicked)
      {
          BeginNewGame();
          var spawn = new Vector2(GameObject.FindWithTag("Spawn").transform.position.x, GameObject.FindWithTag("Spawn").transform.position.y);
          player.transform.position = spawn;
      }
  }

  void BeginNewGame()
  {
      TileMapVisualizer.Clear();
      RunProceduralGeneration();

      var spawn = new Vector2(GameObject.FindWithTag("Spawn").transform.position.x, GameObject.FindWithTag("Spawn").transform.position.y);
      player.transform.position = spawn;

      portal = GameObject.FindWithTag("Portal");
      portal.SetActive(false);
  }

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
    List<Vector2Int> deadEnds = FindDeadEnds(floorPos);
    CreateRoomsAtDeadEnd(deadEnds, roomPos);
    floorPos.UnionWith(roomPos);
    TileMapVisualizer.paintFloorTiles(floorPos);
    WallHandler.createWalls(floorPos, TileMapVisualizer);
  }
  private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPos)
  {
    List<Vector2Int> deadEnds = new List<Vector2Int>();
    foreach (var position in floorPos)
    {
      int neighborsCount = 0;
      foreach (var direction in direction2D.cardinalDirectionsList)
      {
        if(floorPos.Contains(position+direction))
        {
          neighborsCount++;
        }
      }
      if(neighborsCount == 1)
      {
        deadEnds.Add(position);
      }
    }
    return deadEnds;
  }
  private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPos)
  {
    foreach (var position in deadEnds)
    {
      if(roomPos.Contains(position) == false)
      {
        var room = RunRandomWalk(randWalkParams, position);
        roomPos.UnionWith(room);
      }
    }
  }
  private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potRoomPos)
  {
    HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
    int roomCount = Mathf.RoundToInt(potRoomPos.Count*roomPercent);
    List<Vector2Int> roomsToCreate = potRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();
    foreach (var roomPosition in roomsToCreate)
    {
      var roomFloor = RunRandomWalk(randWalkParams, roomPosition);
      SaveRoomData(roomPosition, roomFloor);
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
    corPos = new HashSet<Vector2Int>(floorPos);
  }
  private void ClearRoomData()
  {
      roomsDict.Clear();
  }
  private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
  {
      roomsDict[roomPosition] = roomFloor;
  }
}
