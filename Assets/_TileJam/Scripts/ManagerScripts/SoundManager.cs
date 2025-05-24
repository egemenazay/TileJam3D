using System;
using _TileJam.Scripts.KeyScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TileJam.Scripts.ManagerScripts
{
    public enum SoundType
    {
        LevelCompleteSound,
        LevelFailSound,
        CoinSound,
        TapSound
    }
    [Serializable]
    public class GameSound
    {
        [SerializeField] public AudioClip clip;
        [SerializeField] public SoundType soundType;
    }
    public class SoundManager : MonoBehaviour
    {
        //Gameplay Music, Complete Sound, Fail Sound
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameSound[] gameSounds;

        public static SoundManager Instance;

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
            AudioListener.volume = PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle) == 1 ? 1f : 0f;
        }

        public void PlaySound(SoundType soundType)
        {
            foreach (var gameSound in gameSounds)
            {
                if (gameSound.soundType == soundType)
                {
                    audioSource.PlayOneShot(gameSound.clip);
                    return;
                }
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            Debug.Log("SoundToggle PlayerPref" +PlayerPrefs.GetInt(PlayerPrefKeys.SoundToggle));
            Debug.Log("AudioListener Volume" + AudioListener.volume);
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                PlaySound(SoundType.LevelCompleteSound);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                PlaySound(SoundType.LevelFailSound);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                PlaySound(SoundType.TapSound);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                PlaySound(SoundType.CoinSound);
            }
        }
#endif

        public void CloseSound()
        {
            AudioListener.volume = 0f;
        }

        public void OpenSound()
        {
            AudioListener.volume = 1f;
        }
    }
}
