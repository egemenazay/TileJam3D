using _TileJam.Scripts.KeyScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ManagerScripts
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] private int currentLevelIndex;   //level index value equals scene index 
        [SerializeField] private int totalScenes;  //"0" is loading scene, other values are same with level

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
            totalScenes = SceneManager.sceneCountInBuildSettings;
            GameManager.Instance.OnLevelComplete += OnLevelComplete;
            GameManager.Instance.OnLevelRestart += OnLevelRestart;
            GameManager.Instance.OnLevelFail += OnLevelFail;
            currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex);
            if (currentLevelIndex == 0)
            {
                currentLevelIndex++;
                PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex, currentLevelIndex);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLevelComplete -= OnLevelComplete;
            GameManager.Instance.OnLevelRestart -= OnLevelRestart;
            GameManager.Instance.OnLevelFail -= OnLevelFail;
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

        private void OnLevelFail()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex, currentLevelIndex);
        }

        private void OnLevelRestart()
        {
            if (GameManager.Instance.CurrentGameState == GameState.LevelComplete)
            {
                currentLevelIndex--;
            }
            LoadCurrentScene();  //for now restart is made by a function automatically, this event launched by a button in future
        }
        
        public void LoadCurrentScene()       
        {
            if (currentLevelIndex < totalScenes)
            {
                GameManager.Instance.ChangeGameState(GameState.Gameplay);
                SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
            }
        }
    }
} 
