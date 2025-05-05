using System;
using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.ManagerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _TileJam.Scripts.ViewScripts
{
    public class SettingView : BaseView
    {
        [SerializeField] private Toggle soundToggle;
        public override void Start()
        {
            base.Start();
            //GONNA ADD SOUNDMANAGER
            if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0)
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
            }
            
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle)==0)
            {
                AudioListener.volume = 0f;
                soundToggle.isOn = true;
            }
            else
            {
                AudioListener.volume = 1f;
                soundToggle.isOn = false;
            }
        }

        public void CloseSettingsButton()
        {
            UIManager.Instance.SetGameplayView();
            GameManager.Instance.ChangeGameState(GameState.Gameplay);
        }
        public void ToggleSound()
        {
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle) == 1)
            {
                AudioListener.volume =  0f;
                PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 0);
            }
            else
            {
                AudioListener.volume =  1f;
                PlayerPrefs.SetInt(PlayerPrefKeys.SoundToggle, 1);
            }
        }

        private void Update()
        {
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle));
        }
    }
}
