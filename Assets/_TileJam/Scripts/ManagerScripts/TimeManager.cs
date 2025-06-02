using _TileJam.Scripts.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ManagerScripts
{
    public class TimeManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RemoteConfigDummy remoteConfig;
        
        [Header("Info DO NOT CHANGE")]
        [SerializeField] private float startTimeInSeconds;
        public float currentTime;
        public static TimeManager Instance;
        
        private int[] levelTimers;
        
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
            levelTimers = remoteConfig.GetParsedLevelTimers();
            UpdateTimerFromRemoteConfig();
            currentTime = startTimeInSeconds;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    GameManager.Instance.FailLevel(LevelFailType.TimeOut);
                    ResetTimer();
                }
            }
        }
        private void UpdateTimerFromRemoteConfig()
        {
            int currentLevelIndex = LevelManager.Instance.currentLevelIndex;

            if (levelTimers != null && currentLevelIndex >= 0 && currentLevelIndex < levelTimers.Length)
            {
                startTimeInSeconds = levelTimers[currentLevelIndex];
            }
            else
            {
                Debug.LogWarning("Timer not found for current level index, using fallback.");
            }
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ResetTimer();
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void ResetTimer()
        {
            currentTime = startTimeInSeconds;
        }
    }
}
