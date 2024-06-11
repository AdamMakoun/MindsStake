using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    private bool turnedOn = false;
    [SerializeField]
    private GameObject menuHolder;
    void Start()
    {
        menuHolder.SetActive(false);
        Time.timeScale = 1f;
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) TurnOnOffPauseMenu();
    }

    private void TurnOnOffPauseMenu()
    {
        turnedOn = !turnedOn;
        if (turnedOn)
        {
            Time.timeScale = 0;
            GameManager.instance.playerGameObject.GetComponent<PlayerMovement>().walkingAudio.Pause();
            menuHolder.SetActive(true);
        }
        else if (!turnedOn)
        {
            Time.timeScale = 1;
            GameManager.instance.playerGameObject.GetComponent<PlayerMovement>().walkingAudio.UnPause();
            menuHolder.SetActive(false);
        }
    }

    public void Resume()
    {
        turnedOn = false;
        Time.timeScale = 1;
        menuHolder.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
