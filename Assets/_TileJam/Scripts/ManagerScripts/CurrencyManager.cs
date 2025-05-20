using System;
using _TileJam.Scripts.KeyScripts;
using UnityEngine;

namespace _TileJam.Scripts.ManagerScripts
{
    public class CurrencyManager : MonoBehaviour
    {
        [Header("INFO")]
        [SerializeField] private int coinAmount;
        [Header("Parameters")]
        public int coinGainAmount;

        public static CurrencyManager Instance;
        public event Action<int> OnCoinGain;
        public event Action<int> OnCoinLose;

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
            coinAmount = PlayerPrefs.GetInt(PlayerPrefKeys.CoinAmount);
        }

        public void IncreaseCoinAmount(int amount)
        {
            coinAmount += amount;
            PlayerPrefs.SetInt(PlayerPrefKeys.CoinAmount, coinAmount);
            OnCoinGain?.Invoke(coinGainAmount);
        }
        public void DecreaseCoinAmount(int amount)
        {
            coinAmount -= amount;
            PlayerPrefs.SetInt(PlayerPrefKeys.CoinAmount, coinAmount);
            OnCoinLose?.Invoke(coinAmount);
        }
        
        public int GetCoinAmount()
        {
            return coinAmount;
        }
    }
}
