using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    [SerializeField] private GameObject joinPoint;

    
    public enum CorridorDirection
    {
        top = 0,
        right = 1,
        bottom = 2,
        left = 3
    }
    public CorridorDirection corDirection;
    
    public void BlockOffCorridor()
    {
        Debug.Log("Blocking off corridor");
        joinPoint.SetActive(true);
    }
}

