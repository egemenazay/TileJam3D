using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ViewType
{
    LevelComplete,
    LevelFail
}

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject levelFailUI;

    [Header("Info")]
    [SerializeField] private int counter;
    
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
        Debug.Log("Scene Loaded");
    }

    private void Start()
    {
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
        switch (type)
        {
            case ViewType.LevelComplete:
                levelCompleteUI.SetActive(true);
                break;
            case ViewType.LevelFail:
                levelFailUI.SetActive(true);
                break;
        }
    }
}