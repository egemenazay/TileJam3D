using System;
using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ManagerScripts
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Remote Config Reference")]
        [SerializeField] private RemoteConfigDummy remoteConfig;

        [Header("Info")]
        private int[] levelBuildIndices;
        public int currentLevelIndex; 

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
            currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex);

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
            
        }

        private void OnLevelRestart()
        {
            //GONNA REMAKE
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
