
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenuScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
