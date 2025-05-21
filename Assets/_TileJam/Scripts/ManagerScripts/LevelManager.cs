using System;
using _TileJam.Scriptable_Objects;
using _TileJam.Scripts.KeyScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ManagerScripts
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Remote Config Reference")]
        [SerializeField] private RemoteConfigDummy remoteConfig;

        [Header("Info")]
        private int[] levelBuildIndices; // RemoteConfig'ten çekilen build index listesi
        public int currentLevelIndex; // Bu artık RemoteConfig içindeki sırayı gösterir

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
            levelBuildIndices = remoteConfig.GetParsedLevels();

            GameManager.Instance.OnLevelComplete += OnLevelComplete;
            GameManager.Instance.OnLevelRestart += OnLevelRestart;
            GameManager.Instance.OnLevelFail += OnLevelFail;

            // Load saved level index from PlayerPrefs (not actual scene build index)
            currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex, 0);

            // Ensure index is in bounds
            if (currentLevelIndex < 0 || currentLevelIndex >= levelBuildIndices.Length)
            {
                currentLevelIndex = 0;
                PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex, currentLevelIndex);
            }
        }

        private void Update()
        {
            Debug.Log("Current Level Index in RemoteConfig Sequence: " + currentLevelIndex);
            Debug.Log("Actual Build Index to Load: " + GetCurrentBuildIndex());
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLevelComplete -= OnLevelComplete;
            GameManager.Instance.OnLevelRestart -= OnLevelRestart;
            GameManager.Instance.OnLevelFail -= OnLevelFail;
        }

        private void OnLevelComplete()
        {
            currentLevelIndex++;

            if (currentLevelIndex >= levelBuildIndices.Length)
            {
                currentLevelIndex = 0; // Loop back to start
            }

            PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex, currentLevelIndex);
        }

        private void OnLevelFail()
        {
            // Keep same index
            PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex, currentLevelIndex);
        }

        private void OnLevelRestart()
        {
            if (GameManager.Instance.CurrentGameState == GameState.LevelComplete)
            {
                currentLevelIndex--;
                if (currentLevelIndex < 0) currentLevelIndex = 0;
            }
            LoadCurrentScene();
        }

        public void LoadCurrentScene()
        {
            if (currentLevelIndex >= 0 && currentLevelIndex < levelBuildIndices.Length)
            {
                int buildIndexToLoad = levelBuildIndices[currentLevelIndex];
                GameManager.Instance.ChangeGameState(GameState.Gameplay);
                SceneManager.LoadScene(buildIndexToLoad, LoadSceneMode.Single);
            }
            else
            {
                Debug.LogError("Invalid currentLevelIndex: " + currentLevelIndex);
            }
        }

        public int GetCurrentBuildIndex()
        {
            if (levelBuildIndices != null && currentLevelIndex >= 0 && currentLevelIndex < levelBuildIndices.Length)
                return levelBuildIndices[currentLevelIndex];

            return -1;
        }
    }
} 
