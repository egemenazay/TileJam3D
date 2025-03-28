using System.Collections.Generic;
using _TileJam.Scripts;
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
    [SerializeField] private Canvas levelCompleteView;
    [SerializeField] private Canvas levelFailView;
    [SerializeField] private Canvas gameplayView;
    [SerializeField] private TMP_Text currentlevelText;
    
    [Header("Info")]
    [SerializeField]private int fakeLevelIndex = 1; //Level index in UI
    
    private Dictionary<ViewType, Canvas> viewList = new Dictionary<ViewType, Canvas>();
    
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
        AddViewsToList();
    }
    
    private void Start()
    {
        fakeLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.FakeLevelIndex);
        if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0) // When game launches first time there is no saved index,
        {
            fakeLevelIndex++;
            SaveFakeLevelIndex();
        }
        currentlevelText.text = "Level: " + fakeLevelIndex;
        GameManager.Instance.OnLevelComplete += () => OnLoadView(ViewType.LevelComplete);
        GameManager.Instance.OnLevelFail += () => OnLoadView(ViewType.LevelFail);
    }
    
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        viewList[ViewType.LevelComplete].enabled = false;
        viewList[ViewType.LevelFail].enabled = false;
        viewList[ViewType.Gameplay].enabled = true;
        currentlevelText.text = "Level: " + fakeLevelIndex;
        Debug.Log("Scene Loaded");
    }

    private void OnLoadView(ViewType type)
    {
        viewList[ViewType.Gameplay].enabled = false;
        switch (type)
        {
            case ViewType.LevelComplete:
                viewList[ViewType.LevelComplete].enabled = true;
                fakeLevelIndex++;
                SaveFakeLevelIndex();
                break;
            case ViewType.LevelFail:
                viewList[ViewType.LevelFail].enabled = true;
                //TODO: GameManager function LevelFailType will came here and shows why game failed 
                break;
        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
        GameManager.Instance.OnLevelComplete -= () => OnLoadView(ViewType.LevelComplete);
        // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
        GameManager.Instance.OnLevelFail -= () => OnLoadView(ViewType.LevelFail);
    }
    
    
    private void SaveFakeLevelIndex()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.FakeLevelIndex,fakeLevelIndex);
    }

    private void AddViewsToList()
    {
        viewList.Add(ViewType.Gameplay, gameplayView);
        viewList.Add(ViewType.LevelComplete, levelCompleteView);
        viewList.Add(ViewType.LevelFail, levelFailView);
    }
}