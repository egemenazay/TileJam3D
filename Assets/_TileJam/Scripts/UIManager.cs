using _TileJam.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum ViewType
{
    LevelComplete,
    LevelFail,
    Gameplay
}

public class UIManager : MonoBehaviour
{
    [FormerlySerializedAs("levelCompleteUI")]
    [Header("References")]
    [SerializeField] private GameObject levelCompleteView;
    [SerializeField] private GameObject levelFailView;
    [SerializeField] private GameObject gameplayView;
    [SerializeField] private TMP_Text currentlevelText;
    
    [Header("Info")]
    [SerializeField]private int fakeLevelIndex = 1; //Level index in UI
    
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
        levelCompleteView.SetActive(false);
        levelFailView.SetActive(false);
        gameplayView.SetActive(true);
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
        gameplayView.SetActive(false);
        switch (type)
        {
            case ViewType.LevelComplete:
                levelCompleteView.SetActive(true);
                fakeLevelIndex++;
                SaveFakeLevelIndex();
                break;
            case ViewType.LevelFail:
                levelFailView.SetActive(true);
                //TODO: GameManager function LevelFailType will came here and shows why game failed 
                break;
        }
    }

    private void SaveFakeLevelIndex()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.FakeLevelIndex,fakeLevelIndex);
    }
}