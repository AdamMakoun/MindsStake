using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    right = 1,
    down = 2,
    left = 3
}
public class DungeonCrawlerController : MonoBehaviour
{

    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    public static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right},
        {Direction.left, Vector2Int.left}
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }
        int iterations = Random.Range(dungeonData.iterationMin,dungeonData.iterationMax);
        
        for(int i = 0; i < iterations; i++)
        {
            foreach(DungeonCrawler crawler in dungeonCrawlers)
            {
                Vector2Int newPosition = crawler.Move(directionMovementMap);
                if(!positionsVisited.Contains(newPosition))
                {
                    positionsVisited.Add(newPosition);
                }
            }
        }

        return positionsVisited;
    }
}
