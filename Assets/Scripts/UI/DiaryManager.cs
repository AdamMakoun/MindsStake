using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    private static DiaryManager instance;
    public static DiaryManager Instance {  get { return instance; } }
    [SerializeField]
    private List<Literature> literatureList = new List<Literature>();
    [SerializeField]
    TMP_Text diaryBookContent;
    [SerializeField]
    GameObject buttonIndexPrefab;
    [SerializeField]
    Transform gridTransform;
    [SerializeField]
    GameObject diary;
    bool isOpened = false;

    //create a saving system for the literature list
    public List<Literature> LiteratureList { get { return literatureList; } }
    public void AddLiterature(Literature literature)
    {
        if (literatureList.Contains(literature) == false)
        {
            literatureList.Add(literature);
            SaveState("literature");
        }
    }

    public void SaveState(string key)
    {
        string json = JsonHelper.ToJson(literatureList.ToArray(), true);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public void LoadState(string key)
    {
        string json = PlayerPrefs.GetString(key, "");

        if (!string.IsNullOrEmpty(json))
        {
            Literature[] array = JsonHelper.FromJson<Literature>(json);
            literatureList = new List<Literature>(array);
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        LoadState("literature");
        UpdateIndexes();
        diary.SetActive(false);
      
    }
    private void Update()
    {
        OpenCloseDiary();
    }
    public void OnIndexClick(Literature book)
    {
        diaryBookContent.text = book.text;
    }
    public void UpdateIndexes()
    {
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Literature item in literatureList)
        {
            GameObject buttonObject = Instantiate(buttonIndexPrefab, gridTransform);
            BookClickHandler clickHandler = buttonObject.GetComponent<BookClickHandler>();
            clickHandler.SetBook(item);
        }
    }

    public void CloseDiary()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpened = false;
            diary.SetActive(false);
        }
    }
    public void OpenCloseDiary()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            isOpened = !isOpened;
            diary.SetActive(isOpened);
            UpdateIndexes();
        }
    }
}
