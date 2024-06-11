using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    public bool visited = false;
    public string roomType;
    public GameObject roomIconPrefab;
    [SerializeField] private GameObject[] objectSpawnPositions;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private GameObject[] enemySpawnPositions;
    [SerializeField] private int enemySpawnChance = 5;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Corridor leftCorridor;

    public Corridor rightCorridor;

    public Corridor topCorridor;

    public Corridor bottomCorridor;

    List<Corridor> corridors;

    private void Start()
    {
        corridors = new List<Corridor>();
        if (RoomController.instance == null)
        {
            Debug.Log("Wrong scene loaded");
            return;
        }

        Corridor[] cor = GetComponentsInChildren<Corridor>();
        foreach (Corridor c in cor)
        {
            corridors.Add(c);
            switch (c.corDirection)
            {
                case Corridor.CorridorDirection.top:
                    topCorridor = c;
                    break;
                case Corridor.CorridorDirection.right:
                    rightCorridor = c;
                    break;
                case Corridor.CorridorDirection.bottom:
                    bottomCorridor = c;
                    break;
                case Corridor.CorridorDirection.left:
                    leftCorridor = c;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
        spawnRandomObjects();
        spawnRandomEnemy();
    }
    public void BlockUnconnectedCorridors()
    {
        foreach (Corridor c in corridors)
        {
            switch (c.corDirection)
            {
                case Corridor.CorridorDirection.top:
                    if(GetTop() == null)
                    {
                        c.BlockOffCorridor();
                    }
                    break;
                case Corridor.CorridorDirection.right:
                    if(GetRight() == null)
                    {
                        c.BlockOffCorridor();
                    }
                    break;
                case Corridor.CorridorDirection.bottom:
                    if(GetBottom() == null)
                    {
                        c.BlockOffCorridor();
                    }
                    break;
                case Corridor.CorridorDirection.left:
                    if(GetLeft() == null)
                    {
                        c.BlockOffCorridor();
                    }
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    } 

    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1,Y);
        }
        return null;
    }
    public Room GetTop() 
    {
        if(RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }
    public void Visit()
    {
        visited = true;

        // Notify the minimap that the room has been visited
        if (MinimapBehavior.Instance != null)
        {
            MinimapBehavior.Instance.VisitRoom(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Visit();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Visit();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height,0));
    }
    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }
    public void spawnRandomObjects()
    {
        if(objectSpawnPositions.Length == 0 || objectsToSpawn.Length == 0)
        {
            return;
        }
        foreach(GameObject obj in objectSpawnPositions)
        {
            if(Random.Range(0, 100) < 10)
            {
                int rand = Random.Range(0, objectsToSpawn.Length);
                Instantiate(objectsToSpawn[rand], obj.transform.position, Quaternion.identity, transform);
            }
        }
        
    }
    public void spawnRandomEnemy()
    {
        if(enemiesToSpawn.Length == 0 || enemySpawnPositions.Length == 0)
        {
            return;
        }
        foreach(GameObject obj in enemySpawnPositions)
        {
            if(Random.Range(0, 100) < enemySpawnChance)
            {
                int rand = Random.Range(0, enemiesToSpawn.Length);

                Instantiate(enemiesToSpawn[rand], obj.transform.position, Quaternion.identity, transform);
            }
        }
    }
}
