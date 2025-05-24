using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.ManagerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ViewScripts
{
    public class GameplayView : BaseView
    {
        [Header("References")]
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text timerText;
        private float currentTime;
        [Header("Info")]
        [SerializeField] private int fakeLevelIndex; //Level index in UI
        private Scene scene;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public override void Start()
        {
            fakeLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.FakeLevelIndex);
            if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0) // When game launches first time there is no saved index,
            {
                fakeLevelIndex = 1;
                SaveFakeLevelIndex();
            }
            currentLevelText.text = "Level: " + fakeLevelIndex;
            settingsButton.onClick.AddListener(OpenSettingsButton);
            restartButton.onClick.AddListener(RestartButton);
            GameManager.Instance.OnLevelComplete += OnLevelComplete;
            GameManager.Instance.OnLevelFail += OnLevelFail;
            scene = SceneManager.GetActiveScene();
        }
        private void Update()
        {
            if (scene.name != SceneKeys.LoadScene)
            {
                currentTime = TimeManager.Instance.currentTime;
                UpdateTimerUI();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            currentLevelText.text = "Level: " + fakeLevelIndex;
            UIManager.Instance.OnLoadView(ViewType.Gameplay, 1);
        }
        private void OnLevelComplete()
        {
            fakeLevelIndex++;
            UIManager.Instance.OnLoadView(ViewType.LevelComplete, 2);   //** when LevelComplete opens  CompleteView
            OnClose();
            SaveFakeLevelIndex();
        }
        private void OnLevelFail()
        {
            UIManager.Instance.OnLoadView(ViewType.LevelFail,2);
            OnClose();
        }
        public void OpenSettingsButton()
        {
            UIManager.Instance.OnLoadView(ViewType.Setting, 2);
            GameManager.Instance.ChangeGameState(GameState.UI);
        }
        public void RestartButton()
        {
            LevelManager.Instance.LoadCurrentScene();
        }
        private void SaveFakeLevelIndex()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FakeLevelIndex,fakeLevelIndex);
        }
        private void UpdateTimerUI()
        {
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(currentTime / 60f);
                int seconds = Mathf.FloorToInt(currentTime % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            restartButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
        }
    }
}
