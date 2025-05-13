using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace _TileJam.Scripts.ViewScripts
{
    public class SettingView : BaseView
    {
        [Header("References")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button soundButton;
        [SerializeField] private Image closedSprite;
        public override void Start()
        {
            base.Start();
            closeButton.onClick.AddListener(CloseSettingsButton);
            soundButton.onClick.AddListener(ChangeSoundButton);
            if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0)
            {
                Debug.Log(PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex));
                PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
            }

            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle) == 1)
            {
                OpenSound();
            }
            else
            {
                CloseSound();
            }
        }
        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        public void CloseSettingsButton()
        {
            GameManager.Instance.ChangeGameState(GameState.Gameplay);
            OnClose();
        }
        private void ChangeSoundButton()
        {
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle) == 1)
            {
                CloseSound();
            }
            else
            {
                OpenSound();
            }
        }

        private void Update()
        {
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle));
        }

        private void CloseSound()
        {
            AudioListener.volume = 0f;
            closedSprite.enabled = true;
            PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 0);
        }

        private void OpenSound()
        {
            AudioListener.volume = 1f;
            closedSprite.enabled = false;
            PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
        }
    }
}
