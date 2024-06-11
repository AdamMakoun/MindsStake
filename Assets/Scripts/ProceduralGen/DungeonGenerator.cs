using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            if (roomLocation != Vector2Int.zero)
            {
                switch (Random.Range(0, 100))
                {
                    case int n when (n <= 10):
                        RoomController.instance.LoadRoom("Treasure", roomLocation.x, roomLocation.y);
                        break;
                    case int n when (n > 10 && n <= 20):
                        RoomController.instance.LoadRoom("Monster", roomLocation.x, roomLocation.y);
                        break;
                    case int n when (n > 20 && n <= 45):
                        RoomController.instance.LoadRoom("Safe", roomLocation.x, roomLocation.y);
                        break;
                    case int n when (n > 45 && n <= 50):
                        RoomController.instance.LoadRoom("Library", roomLocation.x, roomLocation.y);
                        break;
                    default:
                        RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
                        break;
                }
            }
        } 
    }
}
