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
    [SerializeField] private Button loadButton;

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
    }

    private void Start()
    {
        GameManager.Instance.OnLevelComplete += () => OnLoadView(ViewType.LevelComplete);
        GameManager.Instance.OnLevelFail += () => OnLoadView(ViewType.LevelFail);
        levelCompleteUI = GameObject.Find("Level/UI/LevelCompleteView");
        levelFailUI = GameObject.Find("Level/UI/LevelFailView");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelCompleteUI = GameObject.Find("Level/UI/LevelCompleteView");
        levelFailUI = GameObject.Find("Level/UI/LevelFailView");
        loadButton = null;
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
        loadButton = GameObject.Find("LoadLevelButton").GetComponent<Button>();
        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener((() => LevelManager.Instance.LoadCurrentScene()));
    }
}