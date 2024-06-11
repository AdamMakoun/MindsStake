using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [SerializeField] private GameObject map;
    private bool isOpen = false;
    void Start()
    {
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            OpenClose();
        }
    }
    public void OpenClose()
    {
        isOpen = !isOpen;
        map.SetActive(isOpen);
    }
}
