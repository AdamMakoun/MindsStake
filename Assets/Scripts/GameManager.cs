
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerGameObject;
    public PlayerBehavior playerBehavior;
    public Image stressBar;
    public float intensity = 0.02f;
    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    public HpBarUpdate hpBarUpdate;
    [SerializeField]
    public GameObject loadingPanel;
    [SerializeField]
    public GameObject losePanel;
    [SerializeField]
    public GameObject winPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerBehavior = playerGameObject.GetComponent<PlayerBehavior>();
        Time.timeScale = 1f;
    }
    private void Update()
    {
        UpdateStressBar();
    }
    public void UpdateStressBar()
    {
        stressBar.fillAmount = playerBehavior.Player.StressLevel / playerBehavior.Player.MaxStressLevel;
    }
    public void SetIntensity()
    {
        globalLight.intensity = intensity;
    }
    public void turnOnWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void OnBeforeTransformParentChanged()
    {
        losePanel.SetActive(true);
    }
    
}
