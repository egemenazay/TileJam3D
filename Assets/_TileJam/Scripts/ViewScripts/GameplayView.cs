using System;
using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.ManagerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TileJam.Scripts.ViewScripts
{
    public class GameplayView : BaseView
    {
        [SerializeField] private TMP_Text currentLevelText;
        [Header("Info")]
        [SerializeField] private int fakeLevelIndex; //Level index in UI

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
            GameManager.Instance.OnLevelComplete += OnLevelComplete;
            GameManager.Instance.OnLevelFail += OnLevelFail;
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
        private void SaveFakeLevelIndex()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FakeLevelIndex,fakeLevelIndex);
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
