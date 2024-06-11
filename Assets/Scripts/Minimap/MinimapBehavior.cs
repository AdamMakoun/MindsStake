using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MinimapBehavior : MonoBehaviour
{
    public List<string> roomTypeKeys;
    public List<GameObject> roomIconValues;
    private Dictionary<string, GameObject> roomIconPrefabs = new Dictionary<string, GameObject>(); 
    private Dictionary<Vector2Int, GameObject> roomIcons = new Dictionary<Vector2Int, GameObject>();
    private List<Room> visitedRooms = new List<Room>();
    [SerializeField] private GameObject parentObject;
    private static MinimapBehavior instance;
    public static MinimapBehavior Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MinimapBehavior>();
            }
            return instance;
        }
    }
    void Awake()
    {
        for (int i = 0; i != Mathf.Min(roomTypeKeys.Count, roomIconValues.Count); i++)
            roomIconPrefabs.Add(roomTypeKeys[i], roomIconValues[i]);
    }

    
   
    public void AddRoom(Room room)
    {
        GameObject roomIconPrefab;

        if (!(roomIconPrefabs.TryGetValue(room.roomType, out roomIconPrefab)))
        {
            Debug.LogError("No room icon prefab for room type: " + room.roomType);
            return;
        }

        GameObject roomIcon = Instantiate(roomIconPrefab, parentObject.transform);
        roomIcon.transform.SetParent(parentObject.transform);
        roomIcon.transform.localPosition = new Vector3(room.X * 50, room.Y * 50, 0);
       
        roomIcon.transform.localScale = new Vector3(1, 1, 1);

        roomIcon.SetActive(true);

        roomIcons[new Vector2Int(room.X, room.Y)] = roomIcon;
    }

    public void VisitRoom(Room room)
    {
        visitedRooms.Add(room);
        Vector2Int position = new Vector2Int(room.X, room.Y);
        if (roomIcons.ContainsKey(position))
        {
            roomIcons[position].GetComponentInChildren<TMP_Text>().text = $"{room.roomType[0]}";
        }
    }
    public void ForgetRandomRoom()
    {

        if (visitedRooms.Count == 0)
        {
            Debug.LogError("No room icons to forget");
            return;
        }

        Room randomRoom = visitedRooms[Random.Range(0, visitedRooms.Count)];
        Vector2Int position = new Vector2Int(randomRoom.X, randomRoom.Y);

        if (roomIcons.ContainsKey(position))
        {
            roomIcons[position].GetComponentInChildren<TMP_Text>().text = $"?";
        }

    }
}
