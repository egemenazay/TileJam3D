using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Mevcut Level Indexi tutmak
    //Game Manager'ın eventlerini dinlemek
    //LevelComplete Durumuna göre level indexini arttırmak
    //Level Complete View ve Level Fail View'ın çağırması için LoadCurrentScene methoduna sahip olmak.
    //Bu method mevcut level indexine göre uygun sahneyi yüklemeli.
    
    private int currentLevelIndex;
    public LevelManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();
        currentLevelIndex = scene.buildIndex;
        GameManager.Instance.OnLevelComplete += LoadCurrentScene;
    }
    

    private void LoadCurrentScene()
    {
        currentLevelIndex++;
        SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
    }
} 
