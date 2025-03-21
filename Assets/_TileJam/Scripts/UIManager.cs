using System;
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
    
    private Dictionary<ViewType, Canvas> viewDictionary = new Dictionary<ViewType, Canvas>();
    
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
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        viewDictionary[ViewType.LevelComplete].enabled = false;
        viewDictionary[ViewType.LevelFail].enabled = false;
        viewDictionary[ViewType.Gameplay].enabled = true;
        currentlevelText.text = "Level: " + fakeLevelIndex;
        Debug.Log("Scene Loaded");
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
        viewDictionary[ViewType.Gameplay].enabled = false;
        switch (type)
        {
            case ViewType.LevelComplete:
                viewDictionary[ViewType.LevelComplete].enabled = true;
                fakeLevelIndex++;
                SaveFakeLevelIndex();
                break;
            case ViewType.LevelFail:
                viewDictionary[ViewType.LevelFail].enabled = true;
                //TODO: GameManager function LevelFailType will came here and shows why game failed 
                break;
        }
    }

    private void SaveFakeLevelIndex()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.FakeLevelIndex,fakeLevelIndex);
    }

    private void AddViewsToList()
    {
        viewDictionary.Add(ViewType.Gameplay, gameplayView);
        viewDictionary.Add(ViewType.LevelComplete, levelCompleteView);
        viewDictionary.Add(ViewType.LevelFail, levelFailView);
    }
}