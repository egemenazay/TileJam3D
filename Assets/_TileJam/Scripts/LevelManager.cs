using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private int totalScenes;

    
    private Scene scene;
    public static LevelManager Instance;
    
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
        scene = SceneManager.GetActiveScene();
        currentLevelIndex = scene.buildIndex;
        totalScenes = SceneManager.sceneCountInBuildSettings;
        if (currentLevelIndex == 0)
        {
            currentLevelIndex++;
            StartCoroutine(LoadSceneCoroutine(currentLevelIndex));   
        }
        GameManager.Instance.OnLevelComplete += OnLevelComplete;
        GameManager.Instance.OnLevelRestart += OnLevelRestart;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelComplete -= OnLevelComplete;
        GameManager.Instance.OnLevelRestart -= OnLevelRestart;
    }

    private void OnLevelComplete()
    {
        currentLevelIndex++;
    }


    private void OnLevelRestart()
    {
        if (GameManager.Instance.CurrentGameState == GameState.LevelComplete)
        {
            currentLevelIndex--;
        }
        LoadCurrentScene();  //for now restart is made by a function automatically, this event launched by a button in future
    }

    private void Update()
    {
        Debug.Log(GameManager.Instance.CurrentGameState);
    }

    public void LoadCurrentScene()       //This functions used in buttons 
    {
        if (currentLevelIndex < totalScenes)
        {
            SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("Son sahneye ulaşıldı, başka sahne yok!");
        }
        GameManager.Instance.CurrentGameState = GameState.Gameplay;
    }
    
    private IEnumerator LoadSceneCoroutine(int sceneBuildIndex)
    {
        if (scene.buildIndex != 0)
        {
            SceneManager.LoadScene("LoadScene");
        }
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
} 
