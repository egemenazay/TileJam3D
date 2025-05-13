using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ManagerScripts
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Timer Settings")]
        [SerializeField] private float startTimeInSeconds = 60f;
        [Header("Info DO NOT CHANGE")]
        public float currentTime;
        public static TimeManager Instance;

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
