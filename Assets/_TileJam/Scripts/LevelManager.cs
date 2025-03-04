using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Mevcut Level Indexi tutmak
    //Game Manager'ın eventlerini dinlemek
    //LevelComplete Durumuna göre level indexini arttırmak
    //Level Complete View ve Level Fail View'ın çağırması için LoadCurrentScene methoduna sahip olmak.
    //Bu method mevcut level indexine göre uygun sahneyi yüklemeli.
    
    public int currentLevelIndex;
    private int totalScenes;
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
        GameManager.Instance.OnLevelComplete += LevelComplete;
    }

    private void LevelComplete()
    {
        currentLevelIndex++;
    }
    
    public void LoadCurrentScene()       //OnLevelComplete Eventine eklenen method; UIManager Gelince içeriği değişecek
    {
        if (currentLevelIndex < totalScenes)
        {
            SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("Son sahneye ulaşıldı, başka sahne yok!");
        }
        GameManager.Instance.gameState = GameManager.GameState.Gameplay;
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
