using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y; 
}
public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentFloorName = "1stFloor";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedLadderRoom = false;

    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if(loadRoomQueue.Count == 0)
        {
            if(!spawnedLadderRoom)
            {
                StartCoroutine(SpawnLadderRoom());
            }
            else if(spawnedLadderRoom && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.BlockUnconnectedCorridors();
                }
                updatedRooms = true;
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    IEnumerator SpawnLadderRoom()
    {
        spawnedLadderRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            Room ladderRoom = loadedRooms[loadedRooms.Count-1];
            Room tempRoom = new Room(ladderRoom.X,ladderRoom.Y);
            Destroy(ladderRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
            GameManager.instance.playerGameObject.GetComponent<PlayerMovement>().enabled = true;
            GameManager.instance.loadingPanel.SetActive(false);
        }
    }
    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
   
    }
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentFloorName + info.name; 

        AsyncOperation loadRoomRequest = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while(loadRoomRequest.isDone == false)
        {
            yield return null;
        }
        

    }
    public void RegisterRoom(Room room) 
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3
            (
                currentLoadRoomData.X * room.width,
                currentLoadRoomData.Y * room.height,
                0
            );



            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentFloorName + " " + currentLoadRoomData.X + "," + currentLoadRoomData.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            loadedRooms.Add(room);
            MinimapBehavior.Instance.AddRoom(room);

        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
    public bool DoesRoomExist(int x, int y)
    { 
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
}
