using System.Collections;
using _TileJam.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private int currentLevelIndex;   //level index value equals scene index 
    [SerializeField] private int totalScenes;  //"0" is loading scene, other values are same with level

    public int CurrentLevelIndex
    {
        get { return currentLevelIndex; }
    }

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
        if (currentLevelIndex == 0)   //This "if" used when game starts normally from loading scene
        {
            if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0)  //when game first launched if 0 level saved this "if" starts game at level1
            {
                currentLevelIndex++;
                PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex ,currentLevelIndex);
            }
            currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex);
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
        if (currentLevelIndex < (totalScenes-1))
        {
            currentLevelIndex++;
        }
        else
        {
            currentLevelIndex = 1;
        }
        PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex,currentLevelIndex);
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
            GameManager.Instance.ChangeGameState(GameState.Gameplay);
            SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
        }
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
