using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ViewType
{
    LevelComplete,
    LevelFail,
    Gameplay
}

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject levelFailUI;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private TMP_Text currentlevelText;
    
    [Header("Info")]
    [SerializeField]private int fakeLevelCount;
    
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelCompleteUI.SetActive(false);
        levelFailUI.SetActive(false);
        gameplayUI.SetActive(true);
        currentlevelText.text = "Level: " + fakeLevelCount;
        Debug.Log("Scene Loaded");
    }

    private void Start()
    {
        if (LevelManager.Instance.CurrentLevelIndex == 0)
        {
            gameplayUI.SetActive(false);
        }
        fakeLevelCount = PlayerPrefs.GetInt("LevelIndex");
        currentlevelText.text = "Level: " + fakeLevelCount;
        GameManager.Instance.OnLevelComplete += () => OnLoadView(ViewType.LevelComplete);
        GameManager.Instance.OnLevelFail += () => OnLoadView(ViewType.LevelFail);
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
        GameManager.Instance.OnLevelComplete -= () => OnLoadView(ViewType.LevelComplete);
        // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
        GameManager.Instance.OnLevelFail -= () => OnLoadView(ViewType.LevelFail);
    }

    private void OnLoadView(ViewType type)
    {
        gameplayUI.SetActive(false);
        switch (type)
        {
            case ViewType.LevelComplete:
                levelCompleteUI.SetActive(true);
                fakeLevelCount++;
                break;
            case ViewType.LevelFail:
                levelFailUI.SetActive(true);
                //GameManager function LevelFailType will came here and shows why game failed 
                break;
        }
    }
}