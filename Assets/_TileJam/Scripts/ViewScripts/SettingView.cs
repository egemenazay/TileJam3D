using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace _TileJam.Scripts.ViewScripts
{
    public class SettingView : BaseView
    {
        [Header("References")]
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Button closeButton;

        private bool isToggleOn;
        public override void Start()
        {
            base.Start();
            //GONNA ADD SOUNDMANAGER
            closeButton.onClick.AddListener(CloseSettingsButton);
            if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0)
            {
                Debug.Log(PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex));
                isToggleOn = false;
                PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
            }
            
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle)==0)
            {
                isToggleOn = false;
            }
            else
            {
                isToggleOn = true;
            }
            soundToggle.isOn = !isToggleOn;
            soundToggle.onValueChanged.AddListener(ToggleSound);
        }
        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
            soundToggle.onValueChanged.RemoveAllListeners();
        }

        public void CloseSettingsButton()
        {
            GameManager.Instance.ChangeGameState(GameState.Gameplay);
            OnClose();
        }
        private void ToggleSound(bool isOn)
        {
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle) == 1)
            {
                CloseSound();
            }
            else
            {
                OpenSound();
            }

            isToggleOn = !isOn;
        }


        private void CloseSound()
        {
            AudioListener.volume = 0f;
            isToggleOn = true;
            PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 0);
        }

        private void OpenSound()
        {
            AudioListener.volume = 1f;
            isToggleOn = false;
            PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
        }
    }
}
