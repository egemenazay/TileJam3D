using _TileJam.Scripts.ManagerScripts;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _TileJam.Scripts.UIScripts.UIHelperScripts
{
    public class CoinHolder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text coinAmountText;
        [Header("Info")]
        [SerializeField]private int currentCoinAmount;

        private void Start()
        {
            currentCoinAmount = CurrencyManager.Instance.GetCoinAmount();
            coinAmountText.text = "" + currentCoinAmount;
            CurrencyManager.Instance.OnCoinGain += OnCoinIncrease;
        }

        private void OnDestroy()
        {
            CurrencyManager.Instance.OnCoinGain -= OnCoinIncrease;
        }

        private void OnCoinIncrease(int coinAmount)
        {
            currentCoinAmount += coinAmount;
            UpdateCoinText();
        }

        private void UpdateCoinText()
        {
            Vector3 punch = new Vector3(1.2f, 1.2f, 1);
            coinAmountText.transform.DOPunchScale(punch, 0.5f, 1);
            coinAmountText.text = "" + currentCoinAmount;
        }
    }
}
